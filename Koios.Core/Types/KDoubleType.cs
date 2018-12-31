using System;
using System.Globalization;

namespace Koios.Core.Types
{
    public class KDoubleType : KConcreteType
    {
        public override Type NativeType => typeof(double);

        public override string ToSerialized(object nativeValue)
        {
            return (nativeValue as double?)?.ToString("##0.######", CultureInfo.InvariantCulture);
        }

        public override object ToNative(string serializedValue)
        {
            return double.TryParse(serializedValue, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var nativeValue)
                ? (object)nativeValue
                : null;
        }

        public override string Format(object nativeValue)
        {
            return (nativeValue as double?)?.ToString("#,##0.######", CultureInfo.InvariantCulture);
        }
    }
}
