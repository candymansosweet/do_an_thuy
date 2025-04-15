using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IGenericRepository<TEntity>: IDisposable where TEntity : class
    {
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Delete(TEntity entity);
        TEntity GetById(Guid id);
        List<TEntity> GetAll();
        IQueryable<TEntity> GetQueryable();
    }
}
