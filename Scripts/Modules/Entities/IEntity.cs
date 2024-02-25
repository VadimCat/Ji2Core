using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using JetBrains.Annotations;
using LiteDB;

namespace Ji2.Entities
{
    public interface IEntity
    {
        public long Id { get; }
    }
    
    //Add specification pattern search with includes
    public interface IRepository<TEntity> where TEntity: IEntity
    {
        [CanBeNull]
        public TEntity Get(long key);
        [CanBeNull]
        public TEntity Get(Func<TEntity, bool> predicate);
        
        public IEnumerable<TEntity> GetRange(Func<TEntity, bool> predicate);

        public void Update(TEntity entity);
        public void Update(IEnumerable<TEntity> entities);
        
        public void Add(TEntity entity);
        public void AddRange(IEnumerable<TEntity> entities);

        public void Delete(TEntity entity);
        
        public void DeleteRange(Func<TEntity, bool> predicate);
    }

    public abstract class LiteDbRepository<TEntity>: IRepository<TEntity> where TEntity : IEntity
    {
        private readonly ILiteDatabase _database;
        private readonly ILiteCollection<TEntity> _set;
        
        public LiteDbRepository(ILiteDatabase database)
        {
            _database = database;
            _set = database.GetCollection<TEntity>(BsonAutoId.Int64);
        }
        
        public TEntity Get(long key)
        {
            return _set.FindById(key);
        }

        public TEntity Get(Func<TEntity, bool> predicate)
        {
            Expression<Func<TEntity, bool>> expr = e => predicate(e);
            expr.Compile();
            return _set.FindOne(expr);
        }

        public IEnumerable<TEntity> GetRange(Func<TEntity, bool> predicate)
        {
            Expression<Func<TEntity, bool>> expr = e => predicate(e);
            expr.Compile();
            return _set.Find(expr);
        }

        public void Update(TEntity entity)
        {
            _set.Update(entity);
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            _set.Update(entities);
        }

        public void Add(TEntity entity)
        {
            _set.Insert(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _set.Insert(entities);
        }

        public void Delete(TEntity entity)
        {
            _set.Delete(entity.Id);
        }

        public void DeleteRange(Func<TEntity, bool> predicate)
        {
            Expression<Func<TEntity, bool>> expr = e => predicate(e);
            expr.Compile();
            _set.DeleteMany(expr);
        }
    }
}
