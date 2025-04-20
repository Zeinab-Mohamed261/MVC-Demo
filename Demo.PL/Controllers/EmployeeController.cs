using Demo.BLL.DTO.DepartmentsDtos;
using Demo.BLL.DTO.EmploeesDtos;
using Demo.BLL.Services.Classes;
using Demo.BLL.Services.Intrfaces;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class EmployeeController (IEmployeeService _employeeService,
                                      ILogger<DepartmentController> _logger,
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
        public IActionResult Create(CreatedEmployeeDto employeeDto)
        {
            if (ModelState.IsValid) //Server side validation
            {
                try
                {
                    int result = _employeeService.CreateEmployee(employeeDto);
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
    }
}
