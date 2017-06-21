using Photosphere.JsonRpc.Services.Dto;

namespace Photosphere.JsonRpc.Services
{
    public interface IJsonRpcMethodInvoker
    {
        JsonRpcMethodInvokingResult Invoke(JsonRpcMethod method, object parameters);
    }
}