﻿using GameDeals.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GameDeals.Data2
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal DbContext context;
        internal DbSet<TEntity> dbSet;

        public Repository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Including<TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            return dbSet.Include(property);
        }

        public TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entity)
        {
            var dbEntity = context.Entry(entity);

            if (dbEntity.State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbEntity.State = EntityState.Modified;
        }

        public Expression Expression => dbSet.AsQueryable().Expression;

        public Type ElementType => dbSet.AsQueryable().ElementType;

        public IQueryProvider Provider => dbSet.AsQueryable().Provider;

        public IEnumerator<TEntity> GetEnumerator()
        {
            return dbSet.AsQueryable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return dbSet.AsQueryable().GetEnumerator();
        }
    }
}
