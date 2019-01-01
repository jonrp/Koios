using System.Data.Common;

namespace Koios.Data.Query
{
    public interface IQueryExecute
    {
        DbDataReader Execute();
    }
}
