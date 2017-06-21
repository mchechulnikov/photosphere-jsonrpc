namespace Photosphere.JsonRpc.Models
{
    // ReSharper disable InconsistentNaming
    public class JsonRpcErrorWithData : JsonRpcError
    {
        public JsonRpcErrorWithData(JsonRpcErrorCode code, string message, object value)
            : base(code, message)
        {
            data = value;
        }

        public object data { get; }
    }
}