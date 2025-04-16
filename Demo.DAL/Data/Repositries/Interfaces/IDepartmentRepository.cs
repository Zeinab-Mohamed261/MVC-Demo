using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Repositries.Interfaces
{
    //signature for property
    //بيمثل شكل ال dept repo
    public interface IDepartmentRepository  //code contract
    {
        //Get All
        IEnumerable<Department> GetAll(bool withTracking = false);
        //Geet By Id
        Department GetById(int id);
        //Update
        int Update(Department Entity);  //int عشان بترجع عدد rows اللي حصلها تعديل
        //Delete
        int Delete(Department Entity);  //delete عشان بترجع عدد rows اللي حصلها تعديل
        //Insert
        int Add (Department Entity);    //insert عشان بترجع عدد rows اللي حصلها تعديل
    }
}
