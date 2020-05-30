using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PayMeDAL.Models;
using PayMeDAL.Repositories;

namespace PayMe.Controllers
{
    public class HomeController : Controller
    {
        IUserRepo _userRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<HomeController> _logger;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;

        public HomeController(IUserRepo userRepo, IWebHostEnvironment webHostEnvironment, ILogger<HomeController> logger, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager)
        {
            _userRepo = userRepo;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            //Init applicationuser so i can acces values to show on index page.
            ApplicationUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            return View(user);
        }
       

    }
}
