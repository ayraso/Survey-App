using Microsoft.AspNetCore.Mvc;

namespace SurveyApp.API.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
