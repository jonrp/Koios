using System;
using System.Globalization;

namespace Koios.Core.Types
{
    public class KDatetimeType : KConcreteType
    {
        public override Type NativeType => typeof(DateTime);

        public override string ToSerialized(object nativeValue)
        {
            return (nativeValue as DateTime?)?.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
        }

        public override object ToNative(string serializedValue)
        {
            return DateTime.TryParseExact(serializedValue, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var nativeValue)
                ? (object)nativeValue
                : null;
        }
    }
}
