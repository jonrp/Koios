using System;

namespace Koios.Core.Types
{
    public class KStringType : KConcreteType
    {
        public override Type NativeType => typeof(string);

        public override string ToSerialized(object nativeValue)
        {
            return nativeValue as string;
        }

        public override object ToNative(string serializedValue)
        {
            return serializedValue;
        }
    }
}
