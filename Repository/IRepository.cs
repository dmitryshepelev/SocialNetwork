﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SocialNetwork.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        TEntity GetById(object id);
        void Add(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}