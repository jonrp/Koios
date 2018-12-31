using System;

namespace Koios.Core.Types
{
    public class KGuidType : KConcreteType
    {
        public override Type NativeType => typeof(Guid);

        public override string ToSerialized(object nativeValue)
        {
            return (nativeValue as Guid?)?.ToString("D");
        }

        public override object ToNative(string serializedValue)
        {
            return Guid.TryParseExact(serializedValue, "D", out var nativeValue)
                ? (object)nativeValue
                : null;
        }
    }
}
