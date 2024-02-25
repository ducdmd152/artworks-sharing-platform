using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHubDAO.Interface
{
    public interface IUnitOfWork
    {
        bool IsTransaction { get; }

        Task BeginTransactionAsync();

        Task CommitTransactionAsync();

        void RollbackTransaction();
    }
}
