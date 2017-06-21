using System;
using Photosphere.JsonRpc.Models;

namespace Photosphere.JsonRpc.Exceptions
{
    public class JsonRpcException : Exception
    {
        public JsonRpcException(string message, JsonRpcErrorCode errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public JsonRpcErrorCode ErrorCode { get; }
    }
}