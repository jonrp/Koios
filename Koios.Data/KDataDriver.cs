using Koios.Data.Query;
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
            bool newConnected;
            switch (e.CurrentState)
            {
                case ConnectionState.Open:
                case ConnectionState.Fetching:
                case ConnectionState.Executing:
                    newConnected = true;
                    break;
                default:
                    newConnected = false;
                    break;
            }
            if (newConnected != connected)
            {
                connected = newConnected;
                StatusChange?.Invoke(connected);
            }
        }

        protected virtual void Open() => dbc.Open();

        protected virtual DbTransaction BeginTransaction() => dbc.BeginTransaction();

        protected virtual DbCommand CreateCommand() => dbc.CreateCommand();

        protected virtual string GetParameterPrefix() => "@";

        public virtual void Dispose() => dbc.Dispose();

        private bool connected;
        public bool IsStatusConnected => connected;

        public event Action<bool> StatusChange;

        public IQuerySelect Select(params string[] fieldNames)
        {
            return new KDataQuery(CreateCommand(), GetParameterPrefix(), fieldNames);
        }
    }
}
