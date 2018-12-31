using Koios.Core.Model;
using System;

namespace Koios.Core.Types
{
    public abstract class KConcreteType : KType
    {
        public abstract Type NativeType { get; }

        public virtual object ToStored(object nativeValue)
        {
            return nativeValue;
        }

        public virtual object ToNative(object storedValue)
        {
            return storedValue;
        }

        public abstract string ToSerialized(object nativeValue);

        public abstract object ToNative(string serializedValue);

        public virtual string Format(object nativeValue)
        {
            return ToSerialized(nativeValue);
        }

        public override string ToString()
        {
            return NativeType.FullName;
        }
    }
}
