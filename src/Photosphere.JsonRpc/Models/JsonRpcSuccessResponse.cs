namespace Photosphere.JsonRpc.Models
{
    // ReSharper disable InconsistentNaming
    public class JsonRpcSuccessResponse : JsonRpcHttpDtoBase
    {
        public JsonRpcSuccessResponse(string id, object result) : base(id)
        {
            this.result = result;
        }

        public object result { get; }
    }
}