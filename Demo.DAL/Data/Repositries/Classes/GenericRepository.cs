using Demo.DAL.Data.Repositries.Interfaces;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Repositries.Classes
{
    public class GenericRepository<TEntity>(AppDbContext _dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {

        public int Add(TEntity Entity) //object member method
        {
            _dbContext.Set<TEntity>().Add(Entity);  //added
            //equals to
            //_dbContext.Add(Entity);
            return _dbContext.SaveChanges();  //update database
        }

        public int Delete(TEntity Entity)
        {
            _dbContext.Set<TEntity>().Remove(Entity);  //remove locally [Deleted]
            return _dbContext.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll(bool withtracking = false)
        {
            if (withtracking)
            {
                return _dbContext.Set<TEntity>().ToList();
            }
            else
                return _dbContext.Set<TEntity>().AsNoTracking().ToList();
        }

        public TEntity GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
            //find<TEntity>(id)
        }

        public int Update(TEntity Entity)
        {
            _dbContext.Set<TEntity>().Update(Entity); //update locally [Modified]
            return _dbContext.SaveChanges();
        }
    }
}
