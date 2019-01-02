using Koios.Data.Table;
using System.Data.Common;

namespace Koios.Data
{
    public class KDataDropTable : ITableDrop, ITableExecute
    {
        private readonly DbCommand cmd;

        public KDataDropTable(DbCommand cmd, string table)
        {
            this.cmd = cmd;
            if (!string.IsNullOrEmpty(cmd.CommandText))
            {
                cmd.CommandText += ";";
            }
            cmd.CommandText += "drop table " + table;
        }

        public int Execute()
        {
            return cmd.ExecuteNonQuery();
        }
    }
}
