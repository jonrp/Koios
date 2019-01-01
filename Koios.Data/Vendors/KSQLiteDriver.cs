using System.Data.SQLite;

namespace Koios.Data.Vendors
{
    public class KSQLiteDriver : KDataDriver
    {
        public KSQLiteDriver(string filePath)
            : base(new SQLiteConnection(""))
        {
        }
    }
}
