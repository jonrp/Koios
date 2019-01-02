namespace Koios.Data.Record
{
    public interface IRecordValue : IRecordExecute
    {
        IRecordValue Value(string column, object value);
    }
}
