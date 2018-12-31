using System;

namespace Koios.Core.Model
{
    public interface KType
    {
        Type NativeType { get; }

        object ToStored(object nativeValue);
        object ToNative(object storedValue);

        string ToSerialized(object nativeValue);
        object ToNative(string serializedValue);

        string Format(object nativeValue);
    }
}
