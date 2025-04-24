using AutoMapper;
using Demo.BLL.DTO.EmploeesDtos;
using Demo.BLL.Services.Intrfaces;
using Demo.DAL.Data.Repositries.Interfaces;
using Demo.DAL.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.Classes
{
    public class EmployeeService(IEmployeeRepository _employeeRepository , IMapper _mapper) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking)
        {
            var employees = _employeeRepository.GetAll(withTracking);
            //     Src              =>     Dest
            //IEnumerable<Employee> => IEnumebrale<EmployeeDto>
            var returnedEmployees = _mapper.Map<IEnumerable<Employee> , IEnumerable<EmployeeDto>>(employees);   //Auto Mapper

            #region Manual Mapping
            //var returnedEmployees = employees.Select(emp => new EmployeeDto()
            //{
            //    Id = emp.Id,
            //    Name = emp.Name,
            //    Age = emp.Age,
            //    Email = emp.Email,
            //    Salary = emp.Salary,
            //    IsActive = emp.IsActive,
            //    EmployeeType = emp.EmployeeType.ToString(),
            //    Gender = emp.Gender.ToString()
            //}); 
            #endregion

            return returnedEmployees;
        }

        public IEnumerable<EmployeeDto> SearchEmployeeByName(string name)
        {
            var employees = _employeeRepository.GetEmployeeByName(name.ToLower());
            //     Src              =>     Dest
            //IEnumerable<Employee> => IEnumebrale<EmployeeDto>
            var returnedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);   //Auto Mapper

            #region Manual Mapping
            //var returnedEmployees = employees.Select(emp => new EmployeeDto()
            //{
            //    Id = emp.Id,
            //    Name = emp.Name,
            //    Age = emp.Age,
            //    Email = emp.Email,
            //    Salary = emp.Salary,
            //    IsActive = emp.IsActive,
            //    EmployeeType = emp.EmployeeType.ToString(),
            //    Gender = emp.Gender.ToString()
            //}); 
            #endregion

            return returnedEmployees;
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            //if(employee == null) return null;
            //else
            //{
            //    var returnedEmp = new EmployeeDetailsDto()
            //    {
            //        Id = employee.Id,
            //        Name = employee.Name,
            //        Age = employee.Age,
            //        Email = employee.Email,
            //        Salary = employee.Salary,
            //        IsActive = employee.IsActive,
            //        EmployeeType = employee.EmployeeType.ToString(),
            //        Gender = employee.Gender.ToString(),
            //        PhoneNumber = employee.PhoneNumber,
            //        HiringDate = DateOnly.FromDateTime(employee.HiringDate),
            //        CreatedBy = 1,
            //        LastModifiedBy = 1,
            //    };
            //    return returnedEmp;

            //}

            return employee is null ? null : _mapper.Map<EmployeeDetailsDto>(employee);
        }

        public int CreateEmployee(CreatedEmployeeDto employee)
        {
            var Employee = _mapper.Map<CreatedEmployeeDto , Employee>(employee);
            return _employeeRepository.Add(Employee);
        }

        public bool DeleteEmployee(int id)  //Soft Delete
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                return _employeeRepository.Update(employee) >0 ? true : false;
            }
        }


        public int UpdateEmployee(UpdateEmployeeDto employee)
        {
            
            return _employeeRepository.Update(_mapper.Map<UpdateEmployeeDto, Employee>(employee));
        }
    }
}
