using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Econometer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1 || _session.GetInt32("account") == 2)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {
                return RedirectToAction("Index", "Client");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
