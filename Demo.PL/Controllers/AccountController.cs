using Demo.DAL.Models;
using Demo.PL.ViewModels.Account;
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

        #region Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var user = userManager.FindByEmailAsync(viewModel.Email).Result;
            if( user is not null)
            {
                bool flag = userManager.CheckPasswordAsync(user, viewModel.Password).Result;
                if(flag)
                {
                    var Result = signInManager.PasswordSignInAsync(user, viewModel.Password,viewModel.RememberMe,false).Result;
                    if (Result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "Your Account Is not Allowed");
                    if(Result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "Your Account Is Locked Out");
                    if (Result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index) , "Home");
                    
                    
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login");
            }
            return View(viewModel);
        }

        #endregion

        #region SignOut
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion

        #region ForgetPassword
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        #endregion

        #region SendResetPasswordLink
        [HttpPost]
        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.FindByEmailAsync(viewModel.Email).Result;
                if(user is not null )
                {
                    //Send Email
                }
                
            }
            ModelState.AddModelError(string.Empty, "Invalid Operation");
            return View(nameof(ForgetPassword), viewModel);
        }
        #endregion

    }
}
