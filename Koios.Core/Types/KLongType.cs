using System;
using System.Globalization;

namespace Koios.Core.Types
{
    public class KLongType : KConcreteType
    {
        public override Type NativeType => typeof(long);

        public override string ToSerialized(object nativeValue)
        {
            return (nativeValue as long?)?.ToString("##0", CultureInfo.InvariantCulture);
        }

        public override object ToNative(string serializedValue)
        {
            return long.TryParse(serializedValue, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out var nativeValue)
                ? (object)nativeValue
                : null;
        }

        public override string Format(object nativeValue)
        {
            return (nativeValue as long?)?.ToString("#,##0", CultureInfo.InvariantCulture);
        }
    }
}
