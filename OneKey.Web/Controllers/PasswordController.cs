using Microsoft.AspNetCore.Mvc;

namespace OneKey.Web.Controllers
{
    public class PasswordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
