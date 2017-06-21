using System.Reflection;

namespace Photosphere.JsonRpc.Services.Dto
{
    public class JsonRpcMethod
    {
        public string MethodName { get; set; }

        public MethodInfo Method { get; set; }

        public ParameterInfo[] Parameters => Method?.GetParameters();

        public object Instance { get; set; }
    }
}