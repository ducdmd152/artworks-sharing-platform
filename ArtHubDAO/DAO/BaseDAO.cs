using ArtHubBO.Entities;
using ArtHubDAO.Data;
using ArtHubDAO.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHubDAO.DAO
{
    public class BaseDAO<TEntity> : IBaseDAO<TEntity> where TEntity : BaseEntity
    {
        private const string ErrorMessage = "Haven't any transaction";
        private UnitOfWork unitOfWork;

        public BaseDAO(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork as UnitOfWork;
        }

        public DbSet<TEntity> DbSet
        {
            get
            {
                return this.unitOfWork.Context.Set<TEntity>();
            }
        }

        public async Task AddAsync(TEntity entity)
        {
            if(!this.unitOfWork.IsTransaction)
            {
                throw new InvalidOperationException(ErrorMessage);
            }

            await this.DbSet.AddAsync(entity).ConfigureAwait(false);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            if (!this.unitOfWork.IsTransaction)
            {
                throw new InvalidOperationException(ErrorMessage);
            }

            await this.DbSet.AddRangeAsync(entities).ConfigureAwait(false);
        }

        public void Remove(TEntity entity)
        {
            if (!this.unitOfWork.IsTransaction)
            {
                throw new InvalidOperationException(ErrorMessage);
            }

            this.DbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            if (!this.unitOfWork.IsTransaction)
            {
                throw new InvalidOperationException(ErrorMessage);
            }

            this.DbSet.RemoveRange(entities);
        }

        public TEntity Update(TEntity entity)
        {
            if (!this.unitOfWork.IsTransaction)
            {
                throw new InvalidOperationException(ErrorMessage);
            }

            this.DbSet.Attach(entity);
            return entity;
        }

        public IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities)
        {
            if (!this.unitOfWork.IsTransaction)
            {
                throw new InvalidOperationException(ErrorMessage);
            }

            this.DbSet.AttachRange(entities);
            return entities;
        }
    }
}
