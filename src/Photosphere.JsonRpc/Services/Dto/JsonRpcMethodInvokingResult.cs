using Photosphere.JsonRpc.Models;

namespace Photosphere.JsonRpc.Services.Dto
{
    public class JsonRpcMethodInvokingResult
    {
        public JsonRpcMethodInvokingResult(object result)
        {
            IsError = false;
            Result = result;
        }

        public JsonRpcMethodInvokingResult(JsonRpcError error)
        {
            IsError = true;
            Error = error;
        }

        public bool IsError { get; set; }

        public object Result { get; set; }

        public JsonRpcError Error { get; set; }
    }
}