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
    //primary constructor   .Net8 c#12
    public class DepartmentRepositpry(AppDbContext dbContext) : IDepartmentRepository
    {
        private readonly AppDbContext _dbContext = dbContext;  //private for encapsulation  //null  // readOnly for prevent edit

        
        public int Add(Department Entity) //object member method
        {
            _dbContext.Departments.Add(Entity);  //added
            return _dbContext.SaveChanges();  //update database
        }

        public int Delete(Department Entity)
        {
            _dbContext.Departments.Remove(Entity);  //remove locally [Deleted]
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Department> GetAll(bool withtracking = false)
        {
            if(withtracking)
            {
                return _dbContext.Departments.ToList();
            }
            else
                return _dbContext.Departments.AsNoTracking().ToList();
        }

        public Department GetById(int id)
        {
            return _dbContext.Departments.Find(id);
            //find<Department>(id)
        }

        public int Update(Department Entity)
        {
             _dbContext.Departments.Update(Entity); //update locally [Modified]
             return _dbContext.SaveChanges();
        }
    }
}
