using System;

namespace Koios.Data.Table
{
    public interface ITableAlter
    {
        ITableStatement AddColumn(string columnName, Type type);
        ITableStatement AddStringColumn(string columnName, uint length);
        ITableStatement AlterColumn(string columnName, Type newType);
        ITableStatement AlterStringColumn(string columnName, uint newLength);
        ITableStatement DropColumn(string columnName);
        ITableStatement AddPrimaryKey(string name, params string[] columns);
        ITableStatement DropPrimaryKey(string name);
        ITableStatement AddUniqueConstraint(string name, params string[] columns);
        ITableStatement DropUniqueConstraint(string name);
        ITableStatement AddIndex(string name, params string[] columns);
        ITableStatement DropIndex(string name);
        ITableStatement AddForeignKey(string name, string column, string foreignTable, string foreignColumn);
        ITableStatement DropForeignKey(string name);
    }
}
