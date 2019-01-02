using Koios.Data.Record;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Koios.Data
{
    public class KDataInsert : IRecordInsertInto, IRecordValue, IRecordExecute
    {
        private readonly DbCommand cmd;
        private readonly string table;
        private readonly List<(string column, object value)> insertValues = new List<(string, object)>();

        public KDataInsert(DbCommand cmd, string table)
        {
            this.cmd = cmd;
            this.table = table;
        }
        
        public IRecordValue Value(string column, object value)
        {
            insertValues.Add((column, value));
            return this;
        }

        public int Execute()
        {
            string columns = "";
            string values = "";
            bool append = false;
            foreach (var cv in insertValues)
            {
                if (append)
                {
                    columns += ",";
                    values += ",";
                }
                else
                {
                    append = true;
                }
                columns += cv.column;
                values += "?";
                cmd.Parameters.Add(cv.value ?? DBNull.Value);
            }
            cmd.CommandText = "insert into " + table + " (" + columns + ") values (" + values + ")";
            return cmd.ExecuteNonQuery();
        }
    }
}
