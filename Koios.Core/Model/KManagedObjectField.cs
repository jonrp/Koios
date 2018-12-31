namespace Koios.Core.Model
{
    public class KManagedObjectField : KObjectField
    {
        public bool IsUpdated { get; private set; }

        public object OriginalValue { get; private set; }
        
        public KManagedObjectField(KSchemaField field, object value, bool isUpdated, object originalValue)
            : base(field, value)
        {
            IsUpdated = isUpdated;
            OriginalValue = originalValue;
        }
    }
}
