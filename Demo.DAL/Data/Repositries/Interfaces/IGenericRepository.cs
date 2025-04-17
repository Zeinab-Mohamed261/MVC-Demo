using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Repositries.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        //Get All
        IEnumerable<TEntity> GetAll(bool withTracking = false);
        //Geet By Id
        TEntity GetById(int id);
        //Update
        int Update(TEntity Entity);  //int عشان بترجع عدد rows اللي حصلها تعديل
        //Delete
        int Delete(TEntity Entity);  //delete عشان بترجع عدد rows اللي حصلها تعديل
        //Insert
        int Add(TEntity Entity);    //insert عشان بترجع عدد rows اللي حصلها تعديل
    }
}
