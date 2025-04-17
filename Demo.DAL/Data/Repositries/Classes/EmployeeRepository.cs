using Demo.DAL.Data.Repositries.Interfaces;
using Demo.DAL.Models.EmployeeModel;
using Demo.DAL.Models.EmployeeModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Repositries.Classes
{
    public class EmployeeRepository(AppDbContext dbContext) : GenericRepository<Employee>(dbContext)/*common*/, IEmployeeRepository /*specific To Employee*/
    {
        public IQueryable<Employee> GetEmployeeByAddress(string address)
        {
            throw new NotImplementedException();
        }
    }
}
