namespace UniversitySystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CommonController : Controller
    {
        public IActionResult Error()
        {
            return View();
        }
    }
}
