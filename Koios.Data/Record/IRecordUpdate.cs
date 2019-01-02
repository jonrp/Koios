namespace Koios.Data.Record
{
    public interface IRecordUpdate
    {
        IRecordSet Set(string column, object value);
    }
}
