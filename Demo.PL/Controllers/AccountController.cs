using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        #region Register
        public IActionResult Register()
        {
            return View();
        } 
        #endregion
    }
}
