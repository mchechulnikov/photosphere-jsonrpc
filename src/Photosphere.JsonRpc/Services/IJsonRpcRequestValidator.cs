using Photosphere.JsonRpc.Models;
using Photosphere.JsonRpc.Services.Dto;

namespace Photosphere.JsonRpc.Services
{
    public interface IJsonRpcRequestValidator
    {
        JsonRpcRequestValidationResult Validate(JsonRpcRequest request);
    }
}