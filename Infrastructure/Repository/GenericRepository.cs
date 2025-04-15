using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public abstract partial class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseModel    
    {
        protected readonly ApplicationContext _context;
        public GenericRepository()
        {
        }
        public GenericRepository(ApplicationContext context) // 
        {
            _context = context;
        }
        public virtual TEntity Add(TEntity entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public virtual TEntity DeleteSub(TEntity entity)
        {
            entity.IsDeleted = true;
            _context.Entry(entity).State = EntityState.Modified; // Expected: Modified
            return entity;
        }
        public virtual TEntity Update(TEntity entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual TEntity Delete(TEntity entity)
        {
            entity.DeleteMe();
            _context.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public TEntity? GetById(Guid id)
        {
            return _context.Set<TEntity>().SingleOrDefault(e => e.Id == id && e.IsDeleted == false);
        }

        public virtual List<TEntity> GetAll()
        {
            return _context.Set<TEntity>().Where(e => e.IsDeleted == false).ToList();
        }
        public virtual IQueryable<TEntity> GetQueryable()
        {
            return _context.Set<TEntity>().Where(e => e.IsDeleted == false);
        }
        public virtual int CountTotal()
        {
            return _context.Set<TEntity>().Count(e => e.IsDeleted == false);
        }
        public void Dispose()
        {
           GC.SuppressFinalize(this);
        }
    }
}
