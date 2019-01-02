using Koios.Data.Filter;

namespace Koios.Data.Record
{
    public interface IRecordWhere : IRecordExecute
    {
        IRecordWhere And(IFilterExpression<string, object> expression);
        IRecordWhere Or(IFilterExpression<string, object> expression);
    }
}
