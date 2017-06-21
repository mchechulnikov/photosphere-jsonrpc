using System.Reflection;

namespace Photosphere.JsonRpc.Services
{
    public interface IJsonRpcParametersParser
    {
        object[] Parse(object parametersObject, ParameterInfo[] expectedParameters);
    }
}