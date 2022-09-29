using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TAApplication.Models;
using TAApplication.Areas.Identity.Data;

namespace TAApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<TAUser> _um;

        public HomeController(ILogger<HomeController> logger, UserManager<TAUser> um)
        {
            _logger = logger;
            _um = um;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Applicant")]
        public IActionResult ApplicationCreate()
        {
            return View();
        }

        [Authorize(Roles = "Professor, Admin, Applicant")]
        public IActionResult ApplicationDetails()
        {
            TAUser currUser = _um.GetUserAsync(User).Result;
            int unid = currUser.Unid;
            if (User.IsInRole("Admin") || User.IsInRole("Professor")|| unid == 0000000)
                return View();
            else
                return LocalRedirect("/Identity/Account/AccessDenied");
        }

        [Authorize(Roles = "Professor, Admin")]
        public IActionResult ApplicationList()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}