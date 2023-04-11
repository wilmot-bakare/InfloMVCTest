using System.Collections.Generic;
using System.Threading.Tasks;
using MyApp.Models.Base;

namespace MyApp.Services.Domain.Interfaces.Base
{
    public interface IServiceBase<TEntity> where TEntity : ModelBase
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(int id);

        Task<TEntity> Create(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        void Delete(TEntity entity);
        void DeleteByID(int id);
    }
}