﻿using Demo.DAL.Models.DepartmentModel;
using Demo.DAL.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Repositries.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
       IQueryable<Employee> GetEmployeeByAddress(string address);
    }
}
