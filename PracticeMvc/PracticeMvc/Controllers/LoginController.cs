using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PracticeMvc.Data;
using PracticeMvc.Models;

namespace PracticeMvc.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public LoginController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(EmployeeUser user)
        {
            if(ModelState.IsValid)
            {
                if(applicationDbContext.EmployeesUser.Any(u => u.Username == user.Username))
                {
                    ModelState.AddModelError("Username", "Username already exists");
                    return View(user);
                }

                string hashedPassword = HashPassword(user.PasswordHash);
                user.PasswordHash = hashedPassword;

                applicationDbContext.Add(user);
                applicationDbContext.SaveChanges();

                return RedirectToAction("Login","Login");  //redirectToAction("view name","Controller")
            }
            return View(user);
        }


        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = applicationDbContext.EmployeesUser.FirstOrDefault(u => u.Username == username);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View();
            }
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login","Login");
        }
    }
}
