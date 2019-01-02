using Koios.Data.Filter;
using Koios.Data.Record;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Koios.Data
{
    public class KDataUpdate : IRecordUpdate, IRecordSet, IRecordWhere, IRecordExecute
    {
        private readonly DbCommand cmd;
        private readonly string table;
        private readonly List<(string column, object value)> updateValues = new List<(string, object)>();
        private IFilterExpression<string, object> filter;

        public KDataUpdate(DbCommand cmd, string table)
        {
            this.cmd = cmd;
            this.table = table;
        }

        public IRecordSet Set(string column, object value)
        {
            updateValues.Add((column, value));
            return this;
        }

        public IRecordWhere Where(IFilterExpression<string, object> expression)
        {
            filter = expression;
            return this;
        }

        public IRecordWhere And(IFilterExpression<string, object> expression)
        {
            filter = filter.And(expression);
            return this;
        }

        public IRecordWhere Or(IFilterExpression<string, object> expression)
        {
            filter = filter.Or(expression);
            return this;
        }

        public int Execute()
        {
            cmd.CommandText = "update " + table + " set ";
            bool append = false;
            foreach (var cv in updateValues)
            {
                if (append)
                {
                    cmd.CommandText += ",";
                }
                else
                {
                    append = true;
                }
                cmd.CommandText += cv.column + "=?";
                cmd.Parameters.Add(cv.value ?? DBNull.Value);
            }
            cmd.CommandText += " where ";
            filter.Compile(new KDataFilterCompiler(cmd));
            return cmd.ExecuteNonQuery();
        }
    }
}
