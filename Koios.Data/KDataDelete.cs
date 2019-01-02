using Koios.Data.Filter;
using Koios.Data.Record;
using System.Data.Common;

namespace Koios.Data
{
    public class KDataDelete : IRecordDeleteFrom, IRecordWhere, IRecordExecute
    {
        private readonly DbCommand cmd;
        private readonly string table;
        private IFilterExpression<string, object> filter;

        public KDataDelete(DbCommand cmd, string table)
        {
            this.cmd = cmd;
            this.table = table;
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
            cmd.CommandText = "delete from " + table + " where ";
            filter.Compile(new KDataFilterCompiler(cmd));
            return cmd.ExecuteNonQuery();
        }
    }
}
