namespace Photosphere.JsonRpc.Models
{
    // ReSharper disable InconsistentNaming
    public class JsonRpcRequest : JsonRpcHttpDtoBase
    {
        public JsonRpcRequest(string id) : base(id) { }

        public string method { get; set; }

        public object @params { get; set; }
    }
}