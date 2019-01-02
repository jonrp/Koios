using System;

namespace Koios.Data.Table
{
    public interface ITableCreate
    {
        ITableColumn WithColumn(string columnName, Type type);
        ITableColumn WithStringColumn(string columnName, uint length);
    }
}
