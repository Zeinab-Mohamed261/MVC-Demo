using Demo.DAL.Models;
using Demo.Presentation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : Controller
    {
        #region Register
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public  IActionResult Register(RegisterViewModel viewModel)
        {
            if(ModelState.IsValid) //Server side validation
            {
                var user = new ApplicationUser()
                {
                    UserName = viewModel.Email.Split("@")[0],  //[maha]@gmail.com
                    Email = viewModel.Email,
                    IsAgree = viewModel.IsAgree,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    
                };
                var result = userManager.CreateAsync(user , viewModel.Password).Result;
                if (result.Succeeded)
                    return RedirectToAction("Login");
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(viewModel);
                }
            }
            return View(viewModel);
        }
        #endregion
    }
}
