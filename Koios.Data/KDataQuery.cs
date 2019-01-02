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
        private readonly string[] columns;
        private string table;
        private readonly List<(string column, bool descending)> order = new List<(string, bool)>();
        private IFilterExpression<string, object> filter;

        public KDataQuery(DbCommand cmd, string[] columns)
        {
            this.cmd = cmd;
            this.columns = columns;
        }

        public IQueryFrom From(string table)
        {
            this.table = table;
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

        public IQueryOrderBy OrderBy(string column, bool descending = false)
        {
            order.Add((column, descending));
            return this;
        }

        public IQueryOrderBy ThenBy(string column, bool descending = false)
        {
            return OrderBy(column, descending);
        }

        public DbDataReader Execute()
        {
            cmd.CommandText = "select ";
            if (columns == null || columns.Length == 0)
            {
                cmd.CommandText += "*";
            }
            else
            {
                cmd.CommandText += string.Join(",", columns);
            }
            cmd.CommandText += " from " + table;
            if (filter != null)
            {
                cmd.CommandText += " where ";
                filter.Compile(new KDataFilterCompiler(cmd));
            }
            if (order.Count > 0)
            {
                cmd.CommandText += " order by " + string.Join(",", order.Select(o => o.column + (o.descending ? " desc" : "")));
            }
            return cmd.ExecuteReader();
        }
    }
}
