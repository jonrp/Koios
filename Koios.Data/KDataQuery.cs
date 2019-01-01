using Koios.Data.Filter;
using Koios.Data.Query;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Koios.Data
{
    public class KDataQuery : IQuerySelect, IQueryFrom, IQueryWhere, IQueryOrderBy, IQueryExecute
    {
        private readonly DbCommand cmd;
        private readonly string parameterPrefix;

        private string[] fieldNames;
        private string schemaName;
        private List<(string fieldName, bool descending)> order = new List<(string, bool)>();
        private IFilterExpression<string, object> filter;

        public KDataQuery(DbCommand cmd, string parameterPrefix, string[] fieldNames)
        {
            this.cmd = cmd;
            this.parameterPrefix = parameterPrefix;

            this.fieldNames = fieldNames;
        }

        public IQueryFrom From(string schemaName)
        {
            this.schemaName = schemaName;
            return this;
        }

        public IQueryWhere Where(IFilterExpression<string, object> expression)
        {
            filter = expression;
            return this;
        }

        public IQueryWhere And(IFilterExpression<string, object> expression)
        {
            filter = filter.And(expression);
            return this;
        }

        public IQueryWhere Or(IFilterExpression<string, object> expression)
        {
            filter = filter.Or(expression);
            return this;
        }

        public IQueryOrderBy OrderBy(string fieldName, bool descending = false)
        {
            order.Add((fieldName, descending));
            return this;
        }

        public IQueryOrderBy ThenBy(string fieldName, bool descending = false)
        {
            return OrderBy(fieldName, descending);
        }

        public DbDataReader Execute()
        {
            cmd.CommandText = "select ";
            if (fieldNames == null || fieldNames.Length == 0)
            {
                cmd.CommandText += "*";
            }
            else
            {
                cmd.CommandText += string.Join(",", fieldNames);
            }
            cmd.CommandText += " from " + schemaName;
            if (filter != null)
            {
                filter.Compile(new KDataFilterCompiler(cmd, parameterPrefix));
            }
            if (order.Count > 0)
            {
                cmd.CommandText += " order by " + string.Join(",", order.Select(o => o.fieldName + (o.descending ? " desc" : "")));
            }
            return cmd.ExecuteReader();
        }
    }
}
