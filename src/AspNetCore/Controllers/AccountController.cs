using Microsoft.AspNet.Mvc;

namespace AspNetCore.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }
    }
}
