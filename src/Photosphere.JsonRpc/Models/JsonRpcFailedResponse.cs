namespace Photosphere.JsonRpc.Models
{
    // ReSharper disable InconsistentNaming
    public class JsonRpcFailedResponse : JsonRpcHttpDtoBase
    {
        public JsonRpcFailedResponse(string id, JsonRpcError error) : base(id)
        {
            this.error = error;
        }

        public JsonRpcError error { get; }
    }
}