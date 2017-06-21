namespace Photosphere.JsonRpc.Models
{
    // ReSharper disable InconsistentNaming
    public class JsonRpcError
    {
        public JsonRpcError(JsonRpcErrorCode code, string message)
        {
            this.code = code;
            this.message = message;
        }

        public short code { get; }

        public string message { get; }
    }
}