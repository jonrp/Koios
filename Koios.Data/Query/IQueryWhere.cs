using Koios.Data.Filter;

namespace Koios.Data.Query
{
    public interface IQueryWhere : IQueryExecute
    {
        IQueryWhere And(IFilterExpression<string, object> expression);
        IQueryWhere Or(IFilterExpression<string, object> expression);
        IQueryOrderBy OrderBy(string fieldName, bool descending = false);
    }
}
