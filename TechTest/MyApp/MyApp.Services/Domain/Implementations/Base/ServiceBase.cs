using System.Collections.Generic;
using System.Threading.Tasks;
using MyApp.Data;
using MyApp.Models.Base;
using MyApp.Services.Domain.Interfaces.Base;

namespace MyApp.Services.Domain.Implementations.Base
{
    public abstract class ServiceBase<TEntity> : IServiceBase<TEntity>
        where TEntity : ModelBase
    {
        protected ServiceBase(IDataAccess dataAccess)
        {
            DataAccess = dataAccess;
        }

        private IDataAccess DataAccess { get; }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return DataAccess.GetAll<TEntity>();
        }

        public virtual TEntity GetById(int id)
        {
            return DataAccess.GetById<TEntity>(id);
        }

        public async virtual Task<TEntity> Create(TEntity entity)
        {
            return await DataAccess.Create(entity);
        }

        public async virtual Task<TEntity> Update(TEntity entity)
        {
            return await DataAccess.Update(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            DataAccess.Delete(entity);
        }

        public void DeleteByID(int id)
        {
            DataAccess.DeleteByID<TEntity>(id);
        }
    }
}
