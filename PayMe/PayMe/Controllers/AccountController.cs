using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PayMe.Viewmodels;
using PayMeDAL.Context;
using PayMeDAL.Models;
using PayMeDAL.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayMe.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment webHostEnvironment, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        private Random random = new Random();

        //Generate a random code string made up of letters and numbers. length can be specified when called
        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //Login 
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: false);

                if (signInResult.Succeeded)
                {
                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt.");
            }

            return View(model);
        }

        //logout
        [HttpPost]
        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //registration
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    Email = model.Email,
                    Money = 100, //Setting a default money value, in real life deployment this would be 0 ofc.
                    UniqueCode = RandomString(8)
                };

                var identityResult = await _userManager.CreateAsync(user, model.Password);

                if (identityResult.Succeeded)
                {
                    if (_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListUsers", "Administration");
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }


        //check if email is in use already
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUseAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                ViewBag.Nope = $"{email} is already in use.";
                return Json(true);
            }
            return Json($"{email} is already in use.");
        }

        [Authorize]
        public IActionResult TopUp()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> TopUpAsync(TopUpViewModel model)
        {
            //get current user and his current balance
            var user = await _userManager.GetUserAsync(User);
            var bal = user.Money;
            var newmoney = bal + model.Money; //add up current balance and topup 
            //set the new balance 
            user.Money = newmoney;
            //update the user
            var result = await _userManager.UpdateAsync(user);
            //In case input isn't a number, should be fine as input field is type 'number'
            if (model.Money == 0)
            {
                ViewBag.Nope = "Numbers only! Decimals possible.";
                return View();
            }
            //on succes, go bakc to index
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Nope = "Please try again.";
            return View();
        }

        [Authorize]
        public IActionResult Withdraw()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Withdraw(TopUpViewModel model)
        {
            //get current user and balance
            var user = await _userManager.GetUserAsync(User);
            var bal = user.Money;
            //remove withdrawn money from balance
            var newmoney = bal - model.Money;
            //check whether he can afford it, if not show how much is possible
            if (newmoney < 0)
            {
                ViewBag.Nope = "Not enough money... Max withdrawable money: " + bal;
                return View();
            }
            else
            {
                //set new money
                user.Money = newmoney;
            }
            //update user
            var result = await _userManager.UpdateAsync(user);
            //on succes, back to index
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Nope = "Please try again.";
            return View();
        }

        [Authorize]
        public IActionResult Transaction()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Transaction(TransactionViewModel model)
        {
            //needed inits
            var u = new UserRepo(_context);
            IEnumerable<ApplicationUser> enumerable = u.GetAll();
            List<ApplicationUser> list = enumerable.ToList();

            //Check whether the transaction code input exists
            var match = list.Where(p => p.UniqueCode == model.Code);
            //get the user-id if there's a match
            var getRecipient = match.Select(o => o.Id);
            string[] id = getRecipient.ToArray();
            if(id.Count() == 0)
            {
                ViewBag.Nope = "Please try again.";
                return View();
            }

            //if match isn't empty, run the code
            if(match != null)
            {
                //Update user who sends money
                var user = await _userManager.GetUserAsync(User);
                var bal = user.Money;
                var newmoney = bal - model.Money;
                //Update user who receives money
                var user2 = await _userManager.FindByIdAsync(id[0]);
                var bal2 = user2.Money;
                var newmoney2 = bal2 + model.Money;

                //check wether the sender has the funds to do so
                if (newmoney < 0)
                {
                    ViewBag.Nope = "Not enough money... Max withdrawable money: " + bal;
                    return View();
                }
                else
                {
                    //set both users new balance
                    user.Money = newmoney;
                    user2.Money = newmoney2;
                }

                //actually update the database
                var result = await _userManager.UpdateAsync(user);
                var result2 = await _userManager.UpdateAsync(user2);
                if (result.Succeeded && result2.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.Nope = "Please try again.";
            }

            return View();
        }
    }
}