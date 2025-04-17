using Demo.BLL.DTO.EmploeesDtos;
using Demo.BLL.Services.Intrfaces;
using Demo.DAL.Data.Repositries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.Classes
{
    public class EmployeeService(IEmployeeRepository _employeeRepository) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking)
        {
            var employees = _employeeRepository.GetAll(withTracking);
            var returnedEmployees = employees.Select(emp => new EmployeeDto()
            {
                Id = emp.Id,
                Name = emp.Name,
                Age = emp.Age,
                Email = emp.Email,
                Salary = emp.Salary,
                IsActive = emp.IsActive,
                EmployeeType = emp.EmployeeType.ToString(),
                Gender = emp.Gender.ToString()
            });
            return returnedEmployees;
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if(employee == null) return null;
            else
            {
                var returnedEmp = new EmployeeDetailsDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    IsActive = employee.IsActive,
                    EmployeeType = employee.EmployeeType.ToString(),
                    Gender = employee.Gender.ToString(),
                    PhoneNumber = employee.PhoneNumber,
                    HiringDate = DateOnly.FromDateTime(employee.HiringDate),
                    CreatedBy = 1,
                    LastModifiedBy = 1,
                };
                return returnedEmp;

            }
        }

        public int CreateEmployee(CreatedEmployeeDto employee)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }


        public int UpdateEmployee(UpdateEmployeeDto employee)
        {
            throw new NotImplementedException();
        }
    }
}
