using Photosphere.JsonRpc.Models;

namespace Photosphere.JsonRpc.Services
{
    public interface IJsonRpcRequestHandler
    {
        JsonRpcHttpDtoBase Handle(JsonRpcRequest request);
    }
}