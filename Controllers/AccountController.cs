using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Econometer.Data;
using Econometer.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Econometer.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public AccountController(ApplicationDbContext _context, IHttpContextAccessor httpContextAccessor) {
            context = _context;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            user.Email = Request.Form["Email"];
            user.Password = GetHash(Request.Form["Password"]);

            User loginuser = context.Users.Where(u => u.Email == user.Email).FirstOrDefault<User>();
            
            if (loginuser == null || loginuser.Password != user.Password)
            {
                ModelState.AddModelError("Error", "Fel uppgifter");
                return View();
            }
            else
            {
                HttpContext.Session.SetString("name", loginuser.Name);
                HttpContext.Session.SetInt32("account", loginuser.AccountID);
                HttpContext.Session.SetInt32("userid", loginuser.UserID);
                return RedirectToAction("Index", "Client");
            }
        }
        private static string GetHash(string text)  
        {  
            using(var sha512 = SHA512.Create())  
            {    
                var hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(text));  
                return System.BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();  
            }  
        }
        public IActionResult Error()
        {
            return View();
        }
        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
