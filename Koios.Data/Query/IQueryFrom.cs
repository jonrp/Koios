using Koios.Data.Filter;

namespace Koios.Data.Query
{
    public interface IQueryFrom : IQueryExecute
    {
        IQueryWhere Where(IFilterExpression<string, object> expression);
        IQueryOrderBy OrderBy(string fieldName, bool descending = false);
    }
}
