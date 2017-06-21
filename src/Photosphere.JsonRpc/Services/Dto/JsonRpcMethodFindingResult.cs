using Photosphere.JsonRpc.Models;

namespace Photosphere.JsonRpc.Services.Dto
{
    public class JsonRpcMethodFindingResult
    {
        public JsonRpcMethodFindingResult(bool isMethodExists, JsonRpcMethod method)
        {
            IsMethodExists = isMethodExists;
            Method = method;
        }

        public JsonRpcMethodFindingResult(bool isMethodExists, JsonRpcError error)
        {
            IsMethodExists = isMethodExists;
            Error = error;
        }

        public bool IsMethodExists { get; set; }

        public JsonRpcMethod Method { get; set; }

        public JsonRpcError Error { get; set; }
    }
}