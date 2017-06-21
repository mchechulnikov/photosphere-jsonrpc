namespace Photosphere.JsonRpc.Models
{
    // ReSharper disable InconsistentNaming
    public abstract class JsonRpcHttpDtoBase
    {
        protected JsonRpcHttpDtoBase(string id)
        {
            jsonrpc = "2.0";
            this.id = id;
        }

        public string jsonrpc { get; set; }

        public string id { get; set; }
    }
}