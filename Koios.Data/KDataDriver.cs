using Koios.Data.Query;
using Koios.Data.Record;
using Koios.Data.Table;
using System;
using System.Data;
using System.Data.Common;

namespace Koios.Data
{
    public class KDataDriver : IDisposable
    {
        private readonly DbConnection dbc;
        protected KDataDriver(DbConnection dbc)
        {
            this.dbc = dbc;
            this.dbc.StateChange += OnStateChanged;
            Open();
        }

        private void OnStateChanged(object sender, StateChangeEventArgs e)
        {
            bool isStatusConnected;
            switch (e.CurrentState)
            {
                case ConnectionState.Open:
                case ConnectionState.Fetching:
                case ConnectionState.Executing:
                    isStatusConnected = true;
                    break;
                default:
                    isStatusConnected = false;
                    break;
            }
            if (isStatusConnected != IsStatusConnected)
            {
                IsStatusConnected = isStatusConnected;
                StatusChange?.Invoke(IsStatusConnected);
            }
        }

        protected virtual void Open() => dbc.Open();

        protected DbTransaction BeginTransaction() => dbc.BeginTransaction();

        protected DbCommand CreateCommand() => dbc.CreateCommand();
        
        public virtual void Dispose() => dbc.Dispose();

        public bool IsStatusConnected { get; private set; }

        public event Action<bool> StatusChange;

        public IQuerySelect Select(params string[] columns)
        {
            return new KDataQuery(CreateCommand(), columns);
        }

        public IRecordInsertInto InsertInto(string table)
        {
            return new KDataInsert(CreateCommand(), table);
        }

        public IRecordUpdate Update(string table)
        {
            return new KDataUpdate(CreateCommand(), table);
        }

        public IRecordDeleteFrom DeleteFrom(string table)
        {
            return new KDataDelete(CreateCommand(), table);
        }

        public ITableCreate CreateTable(string table)
        {
            return new KDataCreateTable(CreateCommand(), table);
        }

        public ITableAlter AlterTable(string table)
        {
            return new KDataAlterTable(CreateCommand(), table);
        }

        public ITableDrop DropTable(string table)
        {
            return new KDataDropTable(CreateCommand(), table);
        }
    }
}
