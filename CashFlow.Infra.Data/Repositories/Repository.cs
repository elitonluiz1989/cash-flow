using CashFlow.Contracts;
using CashFlow.Entities.Base;
using CashFlow.Infra.Data.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CashFlow.Infra.Data.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
        where TKey : notnull
    {
        protected readonly SQLiteContext SQLiteContext;
        protected readonly AbstractValidator<TEntity>? Validator;

        public Repository(SQLiteContext dbContext)
        {
            SQLiteContext = dbContext;
        }

        public Repository(
            SQLiteContext dbContext,
            AbstractValidator<TEntity>? validator
        )
        {
            SQLiteContext = dbContext;
            Validator = validator;
        }

        public int Insert(TEntity entity)
        {
            SQLiteContext.Set<TEntity>().Add(entity);
            return SQLiteContext.SaveChanges();
        }

        public int Update(TEntity entity)
        {
            if (entity is WithRecordingDates<TKey>)
            {
                (entity as WithRecordingDates<TKey>)?.SetUpdatedAt();
            }

            DetachLocalEntity(entity);

            SQLiteContext.Entry(entity).State = EntityState.Modified;
            return SQLiteContext.SaveChanges();
        }

        public int Delete(TKey id)
        {
            TEntity entity = Find(id);

            return Delete(entity);
        }

        public int Delete(TEntity entity)
        {
            if (entity is WithSofDelete<TKey>)
            {
                (entity as WithSofDelete<TKey>)?.SetDeletedAt();

                return Update(entity);
            }

            DetachLocalEntity(entity);

            SQLiteContext.Set<TEntity>().Remove(entity);
            return SQLiteContext.SaveChanges();
        }

        public IList<TEntity> Select()
        {
            IQueryable<TEntity> query = SelectQueryBuilder();

            return query.ToList();
        }

        public IList<TEntity> Select(Func<IQueryable<TEntity>, IQueryable<TEntity>>? filter = null)
        {
            IQueryable<TEntity> query = SelectQueryBuilder(filter);

            return query.ToList();
        }

        public IList<TEntity> All()
        {
            return Select();
        }

        public TEntity Find(TKey id)
        {
            IQueryable<TEntity> query = SelectQueryBuilder();

            return query.FirstOrDefault(e => e.Id.Equals(id), Activator.CreateInstance<TEntity>());
        }

        public TEntity Save(TEntity entity)
        {
            Validate(entity);

            if (SQLiteContext.Entry(entity).State == EntityState.Modified)
            {
                Update(entity);
            }
            else
            {
                Insert(entity);
            }

            return entity;
        }

        public void Validate(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (Validator is not null)
            {
                Validator.ValidateAndThrow(entity);
            }
        }

        private IQueryable<TEntity> SelectQueryBuilder(Func<IQueryable<TEntity>, IQueryable<TEntity>>? filter = null)
        {
            IQueryable<TEntity> query = SQLiteContext.Set<TEntity>();

            if (filter != null)
            {
                query = filter(query);
            }

            return query;
        }

        private void DetachLocalEntity(TEntity entity)
        {
            TEntity? localContextEntity = SQLiteContext.Set<TEntity>().Local.FirstOrDefault(e => e.Id.Equals(entity.Id));

            if (localContextEntity != null)
            {
                SQLiteContext.Entry(localContextEntity).State = EntityState.Detached;
            }
        }
    }
}
