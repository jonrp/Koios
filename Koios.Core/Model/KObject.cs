using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Koios.Core.Model
{
    public class KObject : IEnumerable<KObjectField>
    {
        public KSchema Schema { get; private set; }

        protected readonly object[] values;

        public KObject(KSchema schema, object[] values)
        {
            this.values = values;
            foreach (var def in schema.Where(f => f.DefaultValue != null && values[f.Index] == null))
            {
                values[def.Index] = def.DefaultValue;
            }
        }

        public virtual object this[KSchemaField field]
        {
            get => values[field.Index];
            set => throw new NotSupportedException();
        }
        
        public object this[string field]
        {
            get => this[Schema[field]];
            set => this[Schema[field]] = value;
        }

        public IEnumerator<KObjectField> GetEnumerator()
        {
            foreach (var field in Schema)
            {
                yield return new KObjectField(field, values[field.Index]);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
