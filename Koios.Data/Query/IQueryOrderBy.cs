namespace Koios.Data.Query
{
    public interface IQueryOrderBy : IQueryExecute
    {
        IQueryOrderBy ThenBy(string fieldName, bool descending = false);
    }
}
