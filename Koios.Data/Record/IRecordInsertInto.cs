namespace Koios.Data.Record
{
    public interface IRecordInsertInto
    {
        IRecordValue Value(string column, object value);
    }
}
