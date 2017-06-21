using System;
using System.Reflection;
using Photosphere.JsonRpc.Exceptions;
using Photosphere.JsonRpc.Models;
using Photosphere.JsonRpc.Services.Dto;

namespace Photosphere.JsonRpc.Services
{
    public class JsonRpcMethodInvoker : IJsonRpcMethodInvoker
    {
        private readonly IJsonRpcParametersParser _parametersParser;

        public JsonRpcMethodInvoker(IJsonRpcParametersParser parametersParser)
        {
            _parametersParser = parametersParser;
        }

        public JsonRpcMethodInvokingResult Invoke(JsonRpcMethod method, object parameters)
        {
            try
            {
                var paramsArray = _parametersParser.Parse(parameters, method.Parameters);
                var result = method.Method.Invoke(method.Instance, paramsArray);
                return new JsonRpcMethodInvokingResult(result);
            }
            catch (JsonRpcException exception)
            {
                return HandleJsonRpcException(exception);
            }
            catch (TargetInvocationException exception) when (exception.InnerException is JsonRpcException)
            {
                return HandleJsonRpcException((JsonRpcException) exception.InnerException);
            }
            catch (Exception exception)
            {
                return HandleInternalServerException(exception);
            }
        }

        private static JsonRpcMethodInvokingResult HandleJsonRpcException(JsonRpcException exception)
        {
            var error = new JsonRpcError(exception.ErrorCode, exception.Message);
            return new JsonRpcMethodInvokingResult(error);
        }

        private static JsonRpcMethodInvokingResult HandleInternalServerException(Exception exception)
        {
#if DEBUG
            var error = new JsonRpcErrorWithData(new JsonRpcErrorCode(-1), "Internal server error", exception);
#else
            var error = new JsonRpcError(new JsonRpcErrorCode(-1), "Internal server error");
#endif
            return new JsonRpcMethodInvokingResult(error);
        }
    }
}