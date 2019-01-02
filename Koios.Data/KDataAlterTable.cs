using Koios.Data.Table;
using System;
using System.Data.Common;

namespace Koios.Data
{
    public class KDataAlterTable : ITableAlter, ITableStatement, ITableExecute
    {
        private readonly DbCommand cmd;
        private readonly string table;

        public KDataAlterTable(DbCommand cmd, string table)
        {
            this.cmd = cmd;
            this.table = table;
        }

        private void AlterTable()
        {
            if (!string.IsNullOrEmpty(cmd.CommandText))
            {
                cmd.CommandText += ";";
            }
            cmd.CommandText += "alter table " + table + " ";
        }

        public ITableStatement AddColumn(string columnName, Type type)
        {
            return AddColumn(columnName, type, null);
        }

        public ITableStatement AddStringColumn(string columnName, uint length)
        {
            return AddColumn(columnName, typeof(string), length);
        }

        private ITableStatement AddColumn(string columnName, Type type, uint? length)
        {
            AlterTable();
            cmd.CommandText += "add " + columnName + " " + type;
            if (length.HasValue)
            {
                cmd.CommandText += "(" + length + ")";
            }
            return this;
        }

        public ITableStatement AlterColumn(string columnName, Type newType)
        {
            return AlterColumn(columnName, newType, null);
        }

        public ITableStatement AlterStringColumn(string columnName, uint newLength)
        {
            return AlterColumn(columnName, typeof(string), newLength);
        }

        private ITableStatement AlterColumn(string columnName, Type newType, uint? newLength)
        {
            // SQL Server:  alter column
            // My SQL:      modify column
            // Oracle:      modify
            AlterTable();
            cmd.CommandText += "alter column " + columnName + " " + newType;
            if (newLength.HasValue)
            {
                cmd.CommandText += "(" + newLength + ")";
            }
            return this;
        }

        public ITableStatement DropColumn(string columnName)
        {
            AlterTable();
            cmd.CommandText += "drop column " + columnName;
            return this;
        }

        public ITableStatement AddPrimaryKey(string name, params string[] columns)
        {
            // SQL Server:  add constraint <pk_name> primary key
            // Oracle:      add constraint <pk_name> primary key
            // My SQL:      add primary key
            AlterTable();
            cmd.CommandText += "add constraint " + name + " primary key (" + string.Join(",", columns) + ")";
            return this;
        }

        public ITableStatement DropPrimaryKey(string name)
        {
            // SQL Server:  drop constraint <pk_name>
            // Oracle:      drop constraint <pk_name>
            // My SQL:      drop primary key
            AlterTable();
            cmd.CommandText += "drop constraint " + name;
            return this;
        }

        public ITableStatement AddUniqueConstraint(string name, params string[] columns)
        {
            AlterTable();
            cmd.CommandText += "add constraint " + name + " unique (" + string.Join(",", columns) + ")";
            return this;
        }

        public ITableStatement DropUniqueConstraint(string name)
        {
            AlterTable();
            cmd.CommandText += "drop constraint " + name;
            return this;
        }

        public ITableStatement AddIndex(string name, params string[] columns)
        {
            //AlterTable();
            if (!string.IsNullOrEmpty(cmd.CommandText))
            {
                cmd.CommandText += ";";
            }
            cmd.CommandText += "create index " + name + " on " + table + "(" + string.Join(",", columns) + ")";
            return this;
        }

        public ITableStatement DropIndex(string name)
        {
            // SQL Server:  drop index <table_name>.<index_name>
            // Oracle:      drop index <index_name>
            // My SQL:      alter table <table_name> drop index <index_name>
            //AlterTable();
            if (!string.IsNullOrEmpty(cmd.CommandText))
            {
                cmd.CommandText += ";";
            }
            cmd.CommandText += "drop index " + name + " on " + table;
            return this;
        }

        public ITableStatement AddForeignKey(string name, string column, string foreignTable, string foreignColumn)
        {
            AlterTable();
            cmd.CommandText += "add constraint " + name + " foreign key (" + column + ") references " + foreignTable + "(" + foreignColumn + ")";
            return this;
        }

        public ITableStatement DropForeignKey(string name)
        {
            AlterTable();
            cmd.CommandText += "drop constraint " + name;
            return this;
        }

        public int Execute()
        {
            return cmd.ExecuteNonQuery();
        }
    }
}
