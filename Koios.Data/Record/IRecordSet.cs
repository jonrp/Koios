using Koios.Data.Filter;

namespace Koios.Data.Record
{
    public interface IRecordSet : IRecordExecute
    {
        IRecordSet Set(string column, object value);
        IRecordWhere Where(IFilterExpression<string, object> expression);
    }
}
