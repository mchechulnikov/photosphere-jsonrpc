namespace Photosphere.JsonRpc.Models.Enums
{
    public enum JsonRpcReservedErrorCodes : short
    {
        ParseError = -32700,
        InvalidRequest = -32600,
        MethodNorFound = -32601,
        InvalidParams = -32602,
        InternalError = -32603,
        MinServerError = -32099,
        MaxServerError = -32000
    }
}