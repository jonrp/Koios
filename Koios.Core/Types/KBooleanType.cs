using System;

namespace Koios.Core.Types
{
    public class KBooleanType : KConcreteType
    {
        public override Type NativeType => typeof(bool);

        public override string ToSerialized(object nativeValue)
        {
            return (nativeValue as bool?)?.ToString();
        }

        public override object ToNative(string serializedValue)
        {
            return bool.TryParse(serializedValue, out var nativeValue)
                ? (object)nativeValue
                : null;
        }
    }
}
