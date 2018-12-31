namespace Koios.Core.Model
{
    public class KSchemaField
    {
        #region Basic

        public string Name { get; private set; }

        public KType Type { get; private set; }

        public string Alias { get; private set; }

        public int Index { get; private set; }

        #endregion

        #region Attributes

        public bool IsSystem { get; private set; }

        public object DefaultValue { get; private set; }

        public bool IsNullable { get; private set; }

        public bool IsReadonly { get; private set; }

        public bool IsIndexed { get; private set; }

        public bool IsUnique { get; private set; }

        public bool IsDeleted { get; private set; }

        #endregion

        public KSchemaField(string name, KType type, string alias, int index,
            bool isSystem, object defaultValue, bool isNullable, bool isReadonly, bool isIndexed, bool isUnique, bool isDeleted)
        {
            Name = name;
            Type = type;
            Alias = alias;
            Index = index;
            IsSystem = isSystem;
            DefaultValue = defaultValue;
            IsNullable = isNullable;
            IsReadonly = isReadonly;
            IsIndexed = isIndexed;
            IsUnique = isUnique;
            IsDeleted = isDeleted;
        }
    }
}
