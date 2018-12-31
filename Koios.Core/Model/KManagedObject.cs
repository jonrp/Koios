using System;
using System.Collections;
using System.Collections.Generic;

namespace Koios.Core.Model
{
    public class KManagedObject : KObject, IEnumerable<KManagedObjectField>
    {
        private struct UpdateTracker
        {
            public object UpdatedValue;
            public bool IsUpdated;
        }

        private int updatedCount;
        private readonly UpdateTracker[] updatedValues;

        public KManagedObject(KSchema schema, object[] values)
            : base(schema, values)
        {
            updatedCount = 0;
            updatedValues = new UpdateTracker[values.Length];
        }

        public KManagedObject(KSchema schema)
            : this(schema, new object[schema.FieldsCount])
        {
            MarkedForInsert = true;
        }

        public bool HasChanged(KSchemaField field)
        {
            return updatedValues[field.Index].IsUpdated;
        }

        public bool HasChanged(string fieldName) => HasChanged(Schema[fieldName]);

        public override object this[KSchemaField field]
        {
            get
            {
                var updated = updatedValues[field.Index];
                return updated.IsUpdated ? updated.UpdatedValue : values[field.Index];
            }
            set
            {
                if (IsDeleted)
                {
                    throw new InvalidOperationException("Object deleted");
                }
                if (value == this[field])
                {
                    return;
                }
                if (value == null && !field.IsNullable)
                {
                    throw new ArgumentNullException(field.Name, "Field is not nullable");
                }
                if (field.IsReadonly && !MarkedForInsert)
                {
                    throw new ArgumentException(field.Name, "Field is readonly");
                }

                if (updatedValues[field.Index].IsUpdated)
                {
                    if (values[field.Index] == value)
                    {
                        updatedCount--;
                        updatedValues[field.Index].IsUpdated = false;
                        updatedValues[field.Index].UpdatedValue = null;
                        return;
                    }
                }
                else
                {
                    updatedCount++;
                    updatedValues[field.Index].IsUpdated = true;
                }

                updatedValues[field.Index].UpdatedValue = value;
                MarkedForDelete = false;
            }
        }

        IEnumerator<KManagedObjectField> IEnumerable<KManagedObjectField>.GetEnumerator()
        {
            foreach (var field in Schema)
            {
                var updated = updatedValues[field.Index];
                yield return new KManagedObjectField(field, updated.IsUpdated ? updated.UpdatedValue : values[field.Index],
                    updated.IsUpdated, values[field.Index]);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool MarkedForInsert { get; private set; }

        public bool MarkedForUpdate => updatedCount > 0;

        public bool MarkedForDelete { get; private set; }

        public bool IsDeleted { get; private set; }

        public void Delete()
        {
            if (IsDeleted)
            {
                throw new InvalidOperationException("Object deleted");
            }
            if (MarkedForInsert)
            {
                throw new InvalidOperationException("Object cannot be deleted");
            }
            Revert();
            MarkedForDelete = true;
        }

        public void Revert()
        {
            if (updatedCount > 0)
            {
                for (int i = 0; i < updatedValues.Length; i++)
                {
                    updatedValues[i].IsUpdated = false;
                    updatedValues[i].UpdatedValue = null;
                }
                updatedCount = 0;
            }
            else
            {
                MarkedForDelete = false;
            }
        }

        public void Commit()
        {
            if (MarkedForDelete)
            {
                IsDeleted = true;
                MarkedForDelete = false;
            }
            else if (MarkedForInsert || MarkedForUpdate)
            {
                for (int i = 0; i < updatedValues.Length; i++)
                {
                    if (updatedValues[i].IsUpdated)
                    {
                        values[i] = updatedValues[i].UpdatedValue;
                        updatedValues[i].IsUpdated = false;
                        updatedValues[i].UpdatedValue = null;
                    }
                }
                updatedCount = 0;
                MarkedForInsert = false;
            }
        }
    }
}
