using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnePageMus.Models;
using OnePageMus.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnePageMus.Controllers
{
    public class AccountController : Controller
    {
        private  SignInManager<AppUser> _sign;
        private UserManager<AppUser> _user;

        public AccountController(SignInManager<AppUser> sign,UserManager<AppUser>user)
        {
            _sign = sign;
            _user = user;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Index(RegisterVm registerVm)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            AppUser appUser = new AppUser
            {
                FirstName = registerVm.FirstName,
                LastName=registerVm.LastName,
                Email=registerVm.Email,
                UserName=registerVm.UserName


        };
            IdentityResult result = await _user.CreateAsync(appUser, registerVm.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("Message", item.Description);
                    return View();

                }

            }
            await _sign.SignInAsync(appUser, true);
            

            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> LogOut()
        {
           await _sign.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> Login(LoginVm loginVm)
        {
            AppUser appUser;
            if (loginVm.EmailOrUsername.Contains("@"))
            {
                appUser =await _user.FindByEmailAsync(loginVm.EmailOrUsername);


            }
            else
            {
                appUser = await _user.FindByNameAsync(loginVm.EmailOrUsername);
            }
            if (appUser==null)
            {
                ModelState.AddModelError("Message", "bos qayidib");
                return View();

            }
            var result = _sign.PasswordSignInAsync(appUser, loginVm.Password, loginVm.RememberMe, true);

            return RedirectToAction("Index","Home");
        }
    }
}
