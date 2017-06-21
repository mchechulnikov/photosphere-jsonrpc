using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Photosphere.JsonRpc.Models;
using Photosphere.JsonRpc.Models.Enums;
using Photosphere.JsonRpc.Services.Dto;

namespace Photosphere.JsonRpc.Services
{
    public class JsonRpcMethodFinder : IJsonRpcMethodFinder
    {
        private readonly IReadOnlyDictionary<string, JsonRpcMethod> _methods;

        public JsonRpcMethodFinder(IJsonRpcController[] controllers)
        {
            _methods = GetMethods(controllers);
        }

        public JsonRpcMethodFindingResult Find(string methodName)
        {
            JsonRpcMethod method;
            if (_methods.TryGetValue(methodName, out method))
            {
                return new JsonRpcMethodFindingResult(true, method);
            }

            var error = new JsonRpcError(JsonRpcReservedErrorCodes.MethodNorFound, $"Method '{methodName}' not found");
            return new JsonRpcMethodFindingResult(false, error);
        }

        private static IReadOnlyDictionary<string, JsonRpcMethod> GetMethods(
            IEnumerable<IJsonRpcController> controllers)
        {
            var methods =
                from controller in controllers
                from method in controller.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                select new JsonRpcMethod
                {
                    MethodName = method.Name,
                    Method = method,
                    Instance = controller
                };
            return methods.ToDictionary(jsonRpcMethod => jsonRpcMethod.MethodName);
        }
    }
}