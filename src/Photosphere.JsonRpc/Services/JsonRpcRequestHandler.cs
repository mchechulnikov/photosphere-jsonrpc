using Photosphere.JsonRpc.Models;

namespace Photosphere.JsonRpc.Services
{
    public class JsonRpcRequestHandler : IJsonRpcRequestHandler
    {
        private readonly IJsonRpcRequestValidator _requestValidator;
        private readonly IJsonRpcMethodFinder _methodFinder;
        private readonly IJsonRpcMethodInvoker _methodInvoker;

        public JsonRpcRequestHandler(
            IJsonRpcRequestValidator requestValidator,
            IJsonRpcMethodFinder methodFinder,
            IJsonRpcMethodInvoker methodInvoker)
        {
            _requestValidator = requestValidator;
            _methodFinder = methodFinder;
            _methodInvoker = methodInvoker;
        }

        public JsonRpcHttpDtoBase Handle(JsonRpcRequest request)
        {
            var valudationResult = _requestValidator.Validate(request);
            if (!valudationResult.IsValid)
            {
                return new JsonRpcFailedResponse(valudationResult.RequestId, valudationResult.Error);
            }

            var methodFindingResult = _methodFinder.Find(request.method);
            if (!methodFindingResult.IsMethodExists)
            {
                return new JsonRpcFailedResponse(request.id, methodFindingResult.Error);
            }

            var invokingResult = _methodInvoker.Invoke(methodFindingResult.Method, request.@params);
            if (invokingResult.IsError)
            {
                return new JsonRpcFailedResponse(request.id, invokingResult.Error);
            }

            return new JsonRpcSuccessResponse(request.id, invokingResult.Result);
        }
    }
}