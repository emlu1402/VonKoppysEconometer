using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Econometer.Data;
using Econometer.Data.Models;
using System.Linq;
using System;
using System.Security.Cryptography;
using Microsoft.AspNet.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Econometer.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;


        public UserController(ApplicationDbContext _context, IHttpContextAccessor httpContextAccessor) {
            context = _context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {
                Models.User.UserViewModel viewModel = new Models.User.UserViewModel();
                viewModel.Users = await context.Users.ToListAsync();
                viewModel.Accounts = await context.Accounts.ToListAsync();

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {   
                Models.User.UserViewModel viewModel = new Models.User.UserViewModel();
                viewModel.Accounts = await context.Accounts.ToListAsync();

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public IActionResult Add(User user)
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {   
                user.Password = GetHash(Request.Form["PasswordClearText"]);
                context.Users.Add(user);
                context.SaveChanges();

                return RedirectToAction("Index", "User");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {   
                var user = context.Users.Where(u => u.UserID == id).FirstOrDefault<User>();
                user.Accounts = context.Accounts.ToList();
                return View(user);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {   
                if (user != null)
                {
                    if(Request.Form["PasswordClearText"] != "")
                    {
                        user.Password = GetHash(Request.Form["PasswordClearText"]);
                    }
                context.Users.Attach(user);
                var entry = context.Entry(user);
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
                }
                return RedirectToAction("Index", "User");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {   
                var user = context.Users.Where(u => u.UserID == id).FirstOrDefault<User>();
                return View(user);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        
        [HttpPost]
        public IActionResult Delete(User userFromDelete)
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {
                if (userFromDelete != null)
                {
                var user = context.Users.Where(u => u.UserID == userFromDelete.UserID).FirstOrDefault<User>();
                user.Name = "Borttagen";
                user.Email = "";
                user.AccountID = 3;
                user.Password = "";
                context.SaveChanges();
                }
                return RedirectToAction("Index", "User");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public IActionResult Error()
        {
            return View();
        }

        private static string GetHash(string text)  
        {  
            using(var sha512 = SHA512.Create())  
            {    
                var hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(text));  
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();  
            }  
        }  
    }
}
