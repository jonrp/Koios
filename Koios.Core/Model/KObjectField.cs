namespace Koios.Core.Model
{
    public class KObjectField
    {
        public KSchemaField Field { get; private set; }
        public object Value { get; private set; }

        public KObjectField(KSchemaField field, object value)
        {
            Field = field;
            Value = value;
        }
    }
}
