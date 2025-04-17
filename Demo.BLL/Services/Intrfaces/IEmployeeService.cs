using Demo.BLL.DTO.EmploeesDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.Intrfaces
{
    public interface IEmployeeService
    {
        //Get All Employees
        IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking = false);
        //Get Employee By Id
        EmployeeDetailsDto? GetEmployeeById(int id);
        //Add Employee
        int CreateEmployee(CreatedEmployeeDto employee) ;
        //Update Employee
        int UpdateEmployee(UpdateEmployeeDto employee);
        //Delete Employee
        bool DeleteEmployee(int id);//Soft Delete
    }
}
