using Demo.DAL.Models;
using Demo.PL.Utilities;
using Demo.PL.ViewModels;
using Demo.PL.ViewModels.Account;
using Demo.Presentation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

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
                    var Token = userManager.GeneratePasswordResetTokenAsync(user).Result;
                    //baseUrl/Account/ResetPassword[Actin]/zm664278@gmail.com/token
                    var ResetPasswordUrl = Url.Action("ResetPassword" , "Account" , new {email = viewModel.Email , Token} , Request.Scheme);
                    //Create Email
                    var email = new Email()
                    {
                        To = viewModel.Email,
                        Subject = "Reset Password",
                        Body = ResetPasswordUrl   //ToDo
                    };
                    //Send Email
                    EmailSetting.SendEmail(email);
                    return RedirectToAction("CheckYourInbox");
                }
                
            }
            ModelState.AddModelError(string.Empty, "Invalid Operation");
            return View(nameof(ForgetPassword), viewModel);
        }
        [HttpGet]
        public IActionResult CheckYourInbox()
        {
            return View();
        }
        [HttpGet]
        //https://localhost:7009/Account/ResetPassword?email=zm664278@gmail.com&Token=CfDJ8H3WZFtr2upHneXDy6ulHlU9K2bZLils8bYrYAgtiK8Un7noYDEVQxWBovRkD8A1brnhGjMCUCRGr7WLjuUeZL%2FjHV8i%2BlnawX3dOyXxmpv0NbHQ%2B2xs3XIUy3XBQR%2FjLb8xW2g4UHV%2FdHc8FPWGWuVS7MjbtZ8VPuZqA86a1jW4NcNzFFYqcvRePGWq5Th2P5nwD%2FVL9i%2FGaTn7%2BdxsdUuJYIesJSt3pmAeBvWQGwyI
        public IActionResult ResetPassword(string email , string Token)
        {
            TempData["email"] = email;
            TempData["Token"] = Token;
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel viewModel)
        {
            if(!ModelState.IsValid)  return View(viewModel);
            string email = TempData["email"] as string ?? string.Empty ;
            string Token = TempData["Token"] as string ?? string.Empty;

            var user = userManager.FindByEmailAsync(email).Result;
            if (user != null)
            {
               var Result = userManager.ResetPasswordAsync(user , Token , viewModel.Password).Result;
                if(Result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach(var error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(nameof(ResetPassword) , viewModel);
        }
        #endregion

    }
}
