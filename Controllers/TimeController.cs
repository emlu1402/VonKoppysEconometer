using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Econometer.Data;
using Econometer.Data.Models;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;

namespace Econometer.Controllers
{
    public class TimeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public TimeController(ApplicationDbContext _context, IHttpContextAccessor httpContextAccessor) {
            context = _context;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public IActionResult Index()
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {
                var clients = context.Clients.ToList();
                var times = context.Times.ToList();
                var products = context.Products.ToList();

                Models.Time.TimeViewModel viewModel = new Models.Time.TimeViewModel();
                viewModel.Times = times;
                viewModel.Clients = clients;
                viewModel.Products = products;
                
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public IActionResult Select(int id)
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1 || _session.GetInt32("account") == 2)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {
                var clients = context.Clients.ToList();
                var times = context.Times.Where(t => t.ClientID == id).ToList();
                var products = context.Products.ToList();
                var users = context.Users.ToList();

                Models.Time.TimeViewModel viewModel = new Models.Time.TimeViewModel();
                viewModel.Times = times;
                viewModel.Clients = clients;
                viewModel.Products = products;
                viewModel.Users = users;
                
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        [HttpPost]
        public IActionResult Invoice(int id)
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {
                if (context.Times.Any(o => o.ClientID == id))
                {
                    var clients = context.Clients.ToList();
                    var times = context.Times.Where(t => t.ClientID == id).ToList();
                    var products = context.Products.ToList();
                    var users = context.Users.ToList();

                    Models.Time.TimeViewModel viewModel = new Models.Time.TimeViewModel();
                    viewModel.Times = times;
                    viewModel.Clients = clients;
                    viewModel.Products = products;
                    viewModel.Users = users;
                    
                    return View(viewModel);
                }
                else
                {
                    return RedirectToAction("Index", "Client");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public IActionResult PostInvoice(int id, int user, string html)
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {   
                Invoice invoice = new Invoice();
                invoice.Time = DateTime.Now;
                invoice.Html = html;
                invoice.ClientID = id;
                invoice.UserID = _session.GetInt32("userid") ?? default(int);
                context.Invoices.Add(invoice);
                context.SaveChanges();
                return new EmptyResult();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public IActionResult Calculate(int id, int edit)
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {   
                Time time = context.Times.Where(t => t.TimeID == id).FirstOrDefault<Time>();
                if (time != null && time.Endtime != null)
                {
                DateTime timeend = (DateTime)time.Endtime;
                double diff = timeend.Subtract(time.Starttime).TotalHours;
                time.Hours = Math.Ceiling(diff * 4) / 4;
                context.Times.Attach(time);
                var entry = context.Entry(time);
                entry.Property(e => e.Hours).IsModified = true;
                context.SaveChanges();
                }
                if (edit == 0)
                {
                return RedirectToAction("Index", "Client");
                }
                else
                {
                return RedirectToAction("Edit", new { id = edit });
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public IActionResult SetInvoice(int id)
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {   
                Time time = context.Times.Where(t => t.TimeID == id).FirstOrDefault<Time>();
                if (time != null)
                {
                time.Invoiced = true;
                context.Times.Attach(time);
                var entry = context.Entry(time);
                entry.Property(e => e.Hours).IsModified = true;
                context.SaveChanges();
                }
                return RedirectToAction("Index", "Client");
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
                Time time = context.Times.Where(t => t.TimeID == id).FirstOrDefault<Time>();
                time.Users = context.Users.ToList();
                time.Products = context.Products.ToList();

                return View(time);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public IActionResult Edit(Time time)
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {   
                if (time != null)
                {
                context.Times.Attach(time);
                var entry = context.Entry(time);
                context.Entry(time).State = EntityState.Modified;
                context.SaveChanges(); 
                }
                return RedirectToAction("Index", "Client");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        [HttpGet]
        public IActionResult Delete()
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {
                var time = new Time { TimeID = id };
                context.Times.Attach(time);
                context.Times.Remove(time);
                context.SaveChanges();
                return RedirectToAction("Index", "Client");
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
        public IActionResult Start(int id, int product)
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1 || _session.GetInt32("account") == 2)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {   
                Time time = new Time();
                time.Starttime = DateTime.Now;
                time.ClientID = id; 
                time.Invoiced = false;
                time.ProductID = product;
                time.UserID = _session.GetInt32("userid") ?? default(int);
                context.Times.Add(time);
                context.SaveChanges();

                return RedirectToAction("Index", "Client");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public IActionResult Stop(int id, int product)
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1 || _session.GetInt32("account") == 2)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {   
                Time time = context.Times.Where(t => t.ClientID == id && t.Endtime == null && t.ProductID == product).FirstOrDefault<Time>();
                if (time != null)
                {
                time.Endtime = DateTime.Now; 
                context.Times.Attach(time);
                var entry = context.Entry(time);
                entry.Property(e => e.Hours).IsModified = true;
                context.SaveChanges();
                Calculate(time.TimeID, 0);
                }
                return RedirectToAction("Index", "Client");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}