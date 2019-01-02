using Koios.Data.Table;
using System;
using System.Data.Common;

namespace Koios.Data
{
    public class KDataCreateTable : ITableCreate, ITableColumn, ITableExecute
    {
        private readonly DbCommand cmd;
        private bool append = false;

        public KDataCreateTable(DbCommand cmd, string table)
        {
            this.cmd = cmd;
            if (!string.IsNullOrEmpty(cmd.CommandText))
            {
                cmd.CommandText += ";";
            }
            cmd.CommandText += "create table " + table + " (";
        }

        public ITableColumn WithColumn(string columnName, Type type)
        {
            return WithColumn(columnName, type, null);
        }

        public ITableColumn WithStringColumn(string columnName, uint length)
        {
            return WithColumn(columnName, typeof(string), length);
        }

        private ITableColumn WithColumn(string columnName, Type type, uint? length)
        {
            if (append)
            {
                cmd.CommandText += ",";
            }
            else
            {
                append = true;
            }
            cmd.CommandText += columnName + " " + type;
            if (length.HasValue)
            {
                cmd.CommandText += "(" + length + ")";
            }
            return this;
        }

        public int Execute()
        {
            cmd.CommandText += ")";
            return cmd.ExecuteNonQuery();
        }
    }
}
