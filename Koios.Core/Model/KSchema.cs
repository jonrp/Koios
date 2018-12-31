using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Koios.Core.Model
{
    public class KSchema : IEnumerable<KSchemaField>
    {
        public KStore Store { get; private set;}

        public KSchemaType Type { get; private set; }

        public string Name { get; private set; }

        public bool IsAudited { get; private set; }

        public bool IsDeleted { get; private set; }

        private readonly KSchemaField[] fields;
        private readonly Dictionary<string, KSchemaField> fieldsMap;
        
        public KSchema(KStore store, string name, KSchemaType type, bool isAudited, bool isDeleted, params KSchemaField[] fields)
        {
            Store = store;
            Type = type;
            Name = name;
            IsAudited = isAudited;
            IsDeleted = isDeleted;
            this.fields = fields;
            this.fieldsMap = fields.ToDictionary(f => f.Name);
        }

        public int FieldsCount => fields.Length;

        public bool IsDefined(string fieldName)
        {
            // Here we check the IsDeleted flag to handle "virtual" deletion
            return fieldsMap.TryGetValue(fieldName, out var field) && !field.IsDeleted;
        }

        public KSchemaField this[string fieldName] => fieldsMap[fieldName];

        public KSchemaField this[int index] => fields[index];

        public IEnumerator<KSchemaField> GetEnumerator()
        {
            return ((IEnumerable<KSchemaField>)fields).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
