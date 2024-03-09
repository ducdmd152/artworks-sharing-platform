using ArtHubBO.Entities;
using ArtHubDAO.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHubDAO.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private const string ErrorNotOpenTransaction = "You not open transaction yet!";
        private const string ErrorAlreadyOpenTransaction = "Transaction already open";
        private bool isTransaction;
        private ArtHubDbContext context;

        public UnitOfWork()
        {
            this.context = new ArtHubDbContext();
        }

        public bool IsTransaction
        {
            get
            {
                return this.isTransaction;
            }
        }

        internal ArtHubDbContext Context { get => this.context; }

        public async Task BeginTransactionAsync()
        {
            if(this.isTransaction)
            {
                throw new Exception(ErrorAlreadyOpenTransaction);
            }

            isTransaction = true;
        }

        public async Task CommitTransactionAsync()
        {
            if (!this.isTransaction)
            {
                throw new Exception(ErrorNotOpenTransaction);
            }

            await this.context.SaveChangeAsync().ConfigureAwait(false);
            this.isTransaction = false;
        }

        public void RollbackTransaction()
        {
            if (!this.isTransaction)
            {
                throw new Exception(ErrorNotOpenTransaction);
            }

            this.isTransaction = false;

            foreach(var entry in this.context.ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}
