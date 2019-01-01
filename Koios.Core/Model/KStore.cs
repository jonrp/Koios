using System;
using System.Collections.Generic;

namespace Koios.Core.Model
{
    public interface KStore
    {
        string Name { get; }

        bool IsStatusConnected { get; }

        string StatusMessage { get; }

        event Action<bool, string> StatusChange;

        #region User Management

        IEnumerable<string> Users { get; }

        bool TryGetUser(string name, out KUser user);

        void CreateUser(string name);

        void UpdateUser(string name, KAccess access);

        void DeleteUser(string name);

        #endregion

        #region Schema Management

        IEnumerable<string> Schemas { get; }

        bool TryGetSchema(string name, out KSchema schema);

        void CreateSchema(string name, KSchemaType type);

        void UpdateSchema(string name, params KSchemaField[] fields);

        void DeleteSchema(string name);

        #endregion
    }
}
