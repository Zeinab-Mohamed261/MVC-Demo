using Demo.BLL.DTO.DepartmentsDtos;
using Demo.BLL.DTO.EmploeesDtos;
using Demo.BLL.Services.Classes;
using Demo.BLL.Services.Intrfaces;
using Demo.DAL.Models.EmployeeModel;
using Demo.PL.ViewModels;
using Demo.PL.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class EmployeeController (IEmployeeService _employeeService,
                                      ILogger<EmployeeController> _logger,
                                      IWebHostEnvironment _environment) : Controller
    {
        public IActionResult Index()
        {
            var Employees = _employeeService.GetAllEmployees();
            return View(Employees);
        }

        #region Create Department
        [HttpGet]
        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeDto)
        {
            if (ModelState.IsValid) //Server side validation
            {
                try
                {
                    var employeeCreatedDto = new CreatedEmployeeDto()
                    {
                        Name = employeeDto.Name,
                        Address = employeeDto.Address,
                        Age = employeeDto.Age,
                        IsActive = employeeDto.IsActive,
                        Email = employeeDto.Email,
                        EmployeeType = employeeDto.EmployeeType,
                        Gender = employeeDto.Gender,
                        HiringDate = employeeDto.HiringDate,
                        PhoneNumber = employeeDto.PhoneNumber,
                        Salary = employeeDto.Salary,
                    };
                    int result = _employeeService.CreateEmployee(employeeCreatedDto);
                    if (result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee Can't Be Created !!");
                        //return View(employeeDto);  //employeeDto :عشان لو دلت حاجة غلط ميرجعش الفورم فاضى تاني 
                    }
                }
                catch (Exception ex)
                {
                    //log exception
                    if (_environment.IsDevelopment())
                    {
                        //1.Development => log error in console And return same view with error msg
                        ModelState.AddModelError(string.Empty, ex.Message);
                        //return View(employeeDto);
                    }
                    else
                    {
                        //2.Deployment  => log error in file | table in database  And return Error view
                        _logger.LogError(ex.Message);
                        //return View(employeeDto);
                    }
                }
            }
            return View(employeeDto);  //employeeDto :عشان لو دلت حاجة غلط ميرجعش الفورم فاضى تاني 

            //return View();
        }


        #endregion

        #region Details Of Employee
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if(!id.HasValue) return BadRequest();  //400
            var employee = _employeeService.GetEmployeeById(id.Value);
            if(employee is null) return NotFound();  //404
            return View(employee);
        }
        #endregion

        #region Edit Employee
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();  //400
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();  //404
            var employeeDto = new EmployeeViewModel()
            {
                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender) ,//To Convert String to Enum
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType) 
            };
            return View(employeeDto);
        }
        [ValidateAntiForgeryToken]  //id from Route  لازم
        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel viewModel)
        {

            if (!ModelState.IsValid) return View(viewModel);
            try
            {
                var employeeUpdatedDto = new UpdateEmployeeDto()
                {
                    Id = id.Value,
                    Name = viewModel.Name,
                    Address = viewModel.Address,
                    Age = viewModel.Age,
                    IsActive = viewModel.IsActive,
                    Email = viewModel.Email,
                    EmployeeType = viewModel.EmployeeType,
                    Gender = viewModel.Gender,
                    HiringDate = viewModel.HiringDate,
                    PhoneNumber = viewModel.PhoneNumber,
                    Salary = viewModel.Salary,
                };
                int result = _employeeService.UpdateEmployee(employeeUpdatedDto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Can't Be Updated !!");}
            }
            catch (Exception ex)
            {

                //log exception
                if (_environment.IsDevelopment())
                {
                    //1.Development => log error in console And return same view with error msg
                    ModelState.AddModelError(string.Empty, ex.Message);
                    //return View(departmentDto);
                }
                else
                {
                    //2.Deployment  => log error in file | table in database  And return Error view
                    _logger.LogError(ex.Message);
                    //return View(departmentDto);
                }
            }
            return View(viewModel);
        }
        #endregion

        #region Delete Employee
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if(id == 0) return BadRequest();  //400
            try
            {
                var deleted = _employeeService.DeleteEmployee(id);
                if(deleted) 
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee is not Deleted");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                //log exception
                if (_environment.IsDevelopment())
                {
                    //1.Development => log error in console And return same view with error msg
                    ModelState.AddModelError(string.Empty, ex.Message);
                    //return View(employeeDto);
                }
                else
                {
                    //2.Deployment  => log error in file | table in database  And return Error view
                    _logger.LogError(ex.Message);
                    //return View(employeeDto);
                }
                return RedirectToAction(nameof(Index));
            }
        }
        #endregion
    }
}
