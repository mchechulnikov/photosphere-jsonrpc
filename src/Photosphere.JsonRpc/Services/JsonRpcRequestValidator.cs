using System.CodeDom.Compiler;
using Photosphere.JsonRpc.Models;
using Photosphere.JsonRpc.Models.Enums;
using Photosphere.JsonRpc.Services.Dto;

namespace Photosphere.JsonRpc.Services
{
    public class JsonRpcRequestValidator : IJsonRpcRequestValidator
    {
        public JsonRpcRequestValidationResult Validate(JsonRpcRequest request)
        {
            if (request == null)
            {
                var error = new JsonRpcError(JsonRpcReservedErrorCodes.ParseError, "Parse error");
                return new JsonRpcRequestValidationResult(false, error);
            }

            if (request.jsonrpc != "2.0")
            {
                var error = new JsonRpcError(
                    JsonRpcReservedErrorCodes.InvalidRequest,
                    $"JSON RPC protocol version '{request.jsonrpc}' not supported"
                );
                return new JsonRpcRequestValidationResult(false, error);
            }

            if (!IsValidMethodName(request.method))
            {
                var error = new JsonRpcError(
                    JsonRpcReservedErrorCodes.InvalidRequest,
                    $"Invalid method name '{request.method}'"
                );
                return new JsonRpcRequestValidationResult(false, error);
            }

            if (!IsValidRequstId(request.id))
            {
                var error = new JsonRpcError(
                    JsonRpcReservedErrorCodes.InvalidRequest,
                    $"Invalid request identifier '{request.id}'"
                );
                return new JsonRpcRequestValidationResult(false, error);
            }

            return new JsonRpcRequestValidationResult(true, request.id);
        }

        private static bool IsValidMethodName(string methodName)
        {
            if (string.IsNullOrWhiteSpace(methodName))
            {
                return false;
            }
            if (methodName.StartsWith("rpc."))
            {
                return false;
            }
            var codeDomProvider = CodeDomProvider.CreateProvider("C#");
            return codeDomProvider.IsValidIdentifier(methodName);
        }

        private static bool IsValidRequstId(string id) => !string.IsNullOrWhiteSpace(id) && int.TryParse(id, out int _);
    }
}