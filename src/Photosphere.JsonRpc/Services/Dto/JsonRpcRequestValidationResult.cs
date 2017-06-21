using Photosphere.JsonRpc.Models;

namespace Photosphere.JsonRpc.Services.Dto
{
    public class JsonRpcRequestValidationResult
    {
        public JsonRpcRequestValidationResult(bool isValid, string requestId)
        {
            IsValid = isValid;
            RequestId = requestId;
        }

        public JsonRpcRequestValidationResult(bool isValid, JsonRpcError error)
        {
            IsValid = isValid;
            Error = error;
        }

        public string RequestId { get; set; }

        public bool IsValid { get; set; }

        public JsonRpcError Error { get; set; }
    }
}