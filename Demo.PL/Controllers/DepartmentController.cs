using Demo.BLL.DTO;
using Demo.BLL.DTO.DepartmentsDtos;
using Demo.BLL.Services.Intrfaces;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService ,
                                      ILogger<DepartmentController> _logger ,
                                      IWebHostEnvironment _environment) : Controller
    {
        //private readonly IDepartmentService _departmentService = departmentService;

        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();

            return View(departments);
        }
        #region Create Department
        [HttpGet]   //By Default
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto departmentDto)
        {
            if(ModelState.IsValid) //Server side validation
            {
                try
                {
                   int result = _departmentService.AddDepartment(departmentDto);
                    if(result > 0) 
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department Can't Be Created !!");
                        //return View(departmentDto);  //departmentDto :عشان لو دلت حاجة غلط ميرجعش الفورم فاضى تاني 
                    }
                }
                catch (Exception ex)
                {
                    //log exception
                    if(_environment.IsDevelopment())
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
            }
             return View(departmentDto);  //departmentDto :عشان لو دلت حاجة غلط ميرجعش الفورم فاضى تاني 
            
            //return View();
        }


        #endregion

        #region Detais Of Department
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();  //400
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();  //404
            return View(department);
        }
        #endregion

        #region Edit Of Department
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if ((!id.HasValue)) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);

            if(department is null) return NotFound();
            var departmentViewModel = new DepartmentEditViewModel()
            {
                Name = department.Name,
                Code = department.Code,
                Description = department.Descriotion ,
                DateOfCreation = department.CreatedOn
            };
            return View(departmentViewModel);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit([FromRoute] int? id,DepartmentEditViewModel viewModel)
        {

            if(!ModelState.IsValid) return View(viewModel);
            try
            {
                var updatedDepartment = new UpdateDepartmentDto()
                {
                    Id = id.Value,
                    Name = viewModel.Name,
                    Code = viewModel.Code,
                    Description = viewModel.Description,
                    DateOfCreation = viewModel.DateOfCreation
                };
                int result = _departmentService.UpdateDepartment(updatedDepartment);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department Can't Be Created !!");
                    //return View(departmentDto);  //departmentDto :عشان لو دلت حاجة غلط ميرجعش الفورم فاضى تاني 
                }
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

        #region Delete Department
        #region With Form (Hard-Delete)
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    var department = _departmentService.GetDepartmentById(id.Value);
        //    if (department == null) return NotFound();
        //    return View(department);

        //}

        [HttpPost]
        //id not nullable => user already enter on view of delete with her id
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool deleted = _departmentService.DeleteDepartment(id);
                if (deleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department is not Deleted");
                    //Needed To Data of Department
                    return RedirectToAction(nameof(Delete), new { id = id });

                }
            }
            catch (Exception ex)
            {
                //log exception
                if (_environment.IsDevelopment())
                {
                    //1.Development => log error in console And return same view with error msg
                    ModelState.AddModelError(string.Empty, ex.Message);
                    //return View(departmentDto);
                    return RedirectToAction(nameof(Delete), new { id = id });
                }
                else
                {
                    //2.Deployment  => log error in file | table in database  And return Error view
                    _logger.LogError(ex.Message);
                    //return View(departmentDto);
                    return View("ErrorView");
                }
            }
        }
        #endregion
        #region Modal Delete

        #endregion
        #endregion
    }
}
