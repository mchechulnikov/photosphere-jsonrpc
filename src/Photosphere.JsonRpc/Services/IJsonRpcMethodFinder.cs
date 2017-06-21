using Photosphere.JsonRpc.Services.Dto;

namespace Photosphere.JsonRpc.Services
{
    public interface IJsonRpcMethodFinder
    {
        JsonRpcMethodFindingResult Find(string methodName);
    }
}