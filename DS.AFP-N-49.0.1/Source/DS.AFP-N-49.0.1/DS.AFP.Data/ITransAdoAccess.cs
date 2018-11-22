using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DS.AFP.Data
{
    public interface ITransAdoDbAccess : IAdoDbAccess
    {
        void BeginTransaction();
        void BeginTransaction(IsolationLevel _IsolationLevel);
        void CommitTransaction();
        void RollbackTransaction();
    }
}
