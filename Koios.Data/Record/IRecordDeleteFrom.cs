using Koios.Data.Filter;

namespace Koios.Data.Record
{
    public interface IRecordDeleteFrom : IRecordExecute
    {
        IRecordWhere Where(IFilterExpression<string, object> expression);
    }
}
