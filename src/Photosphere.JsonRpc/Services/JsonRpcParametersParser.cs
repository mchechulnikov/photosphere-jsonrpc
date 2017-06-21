using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Photosphere.JsonRpc.Exceptions;
using Photosphere.JsonRpc.Models.Enums;

namespace Photosphere.JsonRpc.Services
{
    public class JsonRpcParametersParser : IJsonRpcParametersParser
    {
        public object[] Parse(object parametersObject, ParameterInfo[] expectedParameters)
        {
            if (expectedParameters == null || !expectedParameters.Any())
            {
                return null;
            }
            if (parametersObject == null)
            {
                throw new JsonRpcException($"Parse error: parameters not parsed", JsonRpcReservedErrorCodes.ParseError);
            }
            var jArray = parametersObject as JArray;
            if (jArray != null)
            {
                return ParseAsJarray(expectedParameters, jArray);
            }

            var jProperties = (parametersObject as JObject);
            if (jProperties == null)
            {
                throw new JsonRpcException($"Invalid parameters: params must be an Array or an Object", JsonRpcReservedErrorCodes.InvalidParams);
            }
            return ParseAsJobject(expectedParameters, jProperties);
        }

        private static object[] ParseAsJarray(IReadOnlyList<ParameterInfo> expectedParameters, JArray jArray)
        {
            if (jArray.Count != expectedParameters.Count)
            {
                throw new JsonRpcException(
                    $"Invalid parameters: expected parameters count is {expectedParameters.Count}, but passed {jArray.Count}",
                    JsonRpcReservedErrorCodes.InvalidParams);
            }

            var result = new object[expectedParameters.Count];
            for (var i = 0; i < jArray.Count; i++)
            {
                var expectedParameter = expectedParameters[i];
                var parsedObject = ParseJtoken(jArray[i], expectedParameter.ParameterType);
                if (parsedObject == null)
                {
                    throw new JsonRpcException(
                        $"Invalid parameters: invalid type of parameter '{expectedParameter.Name}'",
                        JsonRpcReservedErrorCodes.InvalidParams);
                }
                result[i] = parsedObject;
            }
            return result;
        }

        private static object[] ParseAsJobject(IReadOnlyList<ParameterInfo> expectedParameters, JObject jObject)
        {
            var minParamsCount = expectedParameters
                .Count(p => !p.ParameterType.IsGenericType || p.ParameterType.GetGenericTypeDefinition() != typeof(Nullable<>));
            if (jObject.Count < minParamsCount || jObject.Count > expectedParameters.Count)
            {
                throw new JsonRpcException(
                    $"Invalid parameters: expected parameters count is {expectedParameters.Count}, but passed {jObject.Count}",
                    JsonRpcReservedErrorCodes.InvalidParams);
            }

            var result = new object[expectedParameters.Count];
            for (var i = 0; i < jObject.Count; i++)
            {
                var expectedParameter = expectedParameters[i];
                if (!jObject.TryGetValue(expectedParameter.Name, out JToken jToken))
                {
                    var expectedParameterType = expectedParameter.ParameterType;
                    if (expectedParameterType.IsGenericType && expectedParameterType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        continue;
                    }
                    throw new JsonRpcException(
                        $"Invalid parameters: request doesn't contains parameter '{expectedParameter.Name}'",
                        JsonRpcReservedErrorCodes.InvalidParams);
                }
                var parsedObject = ParseJtoken(jToken, expectedParameter.ParameterType);
                if (parsedObject == null)
                {
                    throw new JsonRpcException(
                        $"Invalid parameters: invalid type of parameter '{expectedParameter.Name}'",
                        JsonRpcReservedErrorCodes.InvalidParams);
                }
                result[i] = parsedObject;
            }
            return result;
        }

        private static object ParseJtoken(JToken jToken, Type type)
        {
            try
            {
                return jToken.ToObject(type);
            }
            catch
            {
                return null;
            }
        }
    }
}