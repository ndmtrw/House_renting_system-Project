using System.Diagnostics;
using House_renting_system_Project.Models;
using Microsoft.AspNetCore.Mvc;
using YourProjectName.Models;

namespace House_renting_system_Project.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            var model = new ErrorViewModel
            {
                StatusCode = statusCode
            };

            if (statusCode == 400 || statusCode == 404)
            {
                return View("Error400", model);
            }

            if (statusCode == 401)
            {
                return View("Error401", model);
            }

            return View(model);
        }
    }
}
