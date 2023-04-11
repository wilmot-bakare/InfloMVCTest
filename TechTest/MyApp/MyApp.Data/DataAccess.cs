using System;
using System.Collections.Generic;
using System.Linq;
using MyApp.Models.Base;

namespace MyApp.Data
{
    public class DataAccess : IDataAccess
    {
        public DataAccess(MyAppDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        private readonly MyAppDbContext _dataContext;

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : ModelBase
        {
            return _dataContext.Set<TEntity>().ToList();
        }

        public TEntity GetById<TEntity>(int id) where TEntity : ModelBase
        {
            return GetAll<TEntity>().FirstOrDefault(p => p.Id.Equals(id));
        }

        public TEntity Create<TEntity>(TEntity entity) where TEntity : ModelBase
        {
            _dataContext.Set<TEntity>().Add(entity);
            _dataContext.SaveChangesAsync();
            return entity;
        }

        public TEntity Update<TEntity>(TEntity entity) where TEntity : ModelBase
        {
            var dbEntity = _dataContext.Set<TEntity>().Find(entity.Id);

            if (dbEntity == null)
            {
                throw new NullReferenceException("The entity does not exist in the data store");
            }
            _dataContext.Entry(dbEntity).CurrentValues.SetValues(entity);
            _dataContext.SaveChangesAsync();
            return dbEntity;
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : ModelBase
        {
            var dbSet = _dataContext.Set<TEntity>();
            var originalEntity = dbSet.Find(entity.Id);
            dbSet.Remove(originalEntity);
            _dataContext.SaveChanges();
        }

        public void DeleteByID<TEntity>(int id) where TEntity : ModelBase
        {
            var entity = _dataContext.Set<TEntity>().Find(id);
            if (entity == null)
            {
                throw new NullReferenceException("The entity does not exist in the data store");
            }

            _dataContext.Set<TEntity>().Remove(entity);
            _dataContext.SaveChanges();
        }
    }
}
