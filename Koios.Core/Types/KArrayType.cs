using Koios.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Koios.Core.Types
{
    public class KArrayType : KConcreteType
    {
        private interface ArrayHelper
        {
            object ToArray<I>(IEnumerable<I> input, Func<I, object> converter);
        }

        private class ArrayHelper<T> : ArrayHelper
        {
            public object ToArray<I>(IEnumerable<I> input, Func<I, object> converter)
            {
                return input.Select(i => (T)converter(i)).ToArray();
            }
        }

        private readonly Type nativeType;
        private readonly KType underlyingType;
        private readonly ArrayHelper helper;

        public override Type NativeType => nativeType;

        public KArrayType(KType underlyingType)
        {
            this.nativeType = underlyingType.NativeType.MakeArrayType();
            this.underlyingType = underlyingType;
            this.helper = Activator.CreateInstance(typeof(ArrayHelper<>).MakeGenericType(underlyingType.NativeType)) as ArrayHelper;
        }

        public override string ToSerialized(object nativeValue)
        {
            return (nativeValue is Array array) ? string.Join(",", array.Cast<object>().Select(underlyingType.ToSerialized)) : null;
        }

        public override object ToNative(string serializedValue)
        {
            return !string.IsNullOrEmpty(serializedValue) ? helper.ToArray(serializedValue.Split(','), underlyingType.ToNative) : null;
        }

        public override string Format(object nativeValue)
        {
            return (nativeValue is Array array) ? "[ " + string.Join(", ", array.Cast<object>().Select(underlyingType.ToSerialized)) + " ]" : null;
        }
    }
}
