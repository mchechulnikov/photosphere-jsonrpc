using System;
using Photosphere.JsonRpc.Models.Enums;

namespace Photosphere.JsonRpc.Models
{
    public struct JsonRpcErrorCode
    {
        private const short MinPreDefinedErrorCode = short.MinValue;
        private const short MaxPreDefinedErrorCode = -32000;
        private readonly short _value;

        public JsonRpcErrorCode(int value) : this((short)value) {}

        private JsonRpcErrorCode(short value)
        {
            Validate(value);
            _value = value;
        }

        private JsonRpcErrorCode(JsonRpcReservedErrorCodes value)
        {
            var convertedValue = (short) value;
            Validate(convertedValue);
            _value = convertedValue;
        }

        public static explicit operator JsonRpcErrorCode(short value) => new JsonRpcErrorCode(value);

        public static implicit operator short(JsonRpcErrorCode value) => value._value;

        public static implicit operator JsonRpcErrorCode(JsonRpcReservedErrorCodes value) => new JsonRpcErrorCode(value);

        private static void Validate(short value)
        {
            if (value > MaxPreDefinedErrorCode)
            {
                return;
            }
            if (Enum.IsDefined(typeof(JsonRpcReservedErrorCodes), value))
            {
                return;
            }
            if (!(value >= (short)JsonRpcReservedErrorCodes.MinServerError && value <= (short)JsonRpcReservedErrorCodes.MaxServerError))
            {
                throw new ArgumentOutOfRangeException(
                    "Pre-defined JSON RPC 2.0 error code must be between range " +
                    $"{MinPreDefinedErrorCode} to {MaxPreDefinedErrorCode} and defined in {nameof(JsonRpcReservedErrorCodes)}");
            }
        }
    }
}