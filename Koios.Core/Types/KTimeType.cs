using System;
using System.Globalization;

namespace Koios.Core.Types
{
    public class KTimeType : KConcreteType
    {
        public override Type NativeType => typeof(TimeSpan);

        public override string ToSerialized(object nativeValue)
        {
            return (nativeValue as TimeSpan?)?.ToString(@"hh\:mm\:ss\.fff", CultureInfo.InvariantCulture);
        }

        public override object ToNative(string serializedValue)
        {
            return TimeSpan.TryParseExact(serializedValue, @"hh\:mm\:ss\.fff", CultureInfo.InvariantCulture, out var nativeValue)
                ? (object)nativeValue
                : null;
        }
    }
}
