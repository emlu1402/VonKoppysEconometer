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
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public ClientController(ApplicationDbContext _context, IHttpContextAccessor httpContextAccessor) {
            context = _context;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<IActionResult> Index()
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1 || _session.GetInt32("account") == 2)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {
                Models.Client.ClientViewModel viewModel = new Models.Client.ClientViewModel();
                // var ClientsWithProducts =
                //     from c in context.Clients
                //     join t in context.Times on c.ClientID equals t.ClientID
                //     join p in context.Products on t.ProductID equals p.ProductID
                //     select new {
                //         ProdId = p.Id, // or pc.ProdId
                //         CatId = c.CatId
                //         // other assignments
                //     };
                viewModel.Clients = await context.Clients.ToListAsync();
                viewModel.Products = await context.Products.ToListAsync();

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public IActionResult Add()
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
        public IActionResult Add(Client client)
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {   
                context.Clients.Add(client);
                context.SaveChanges();

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
                Client client = context.Clients.Where(c => c.ClientID == id).FirstOrDefault<Client>();

                return View(client);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public IActionResult Edit(Client client)
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {   
                if (client != null)
                {
                context.Clients.Attach(client);
                var entry = context.Entry(client);
                context.Entry(client).State = EntityState.Modified;
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
                var client = new Client { ClientID = id };
                context.Times.RemoveRange(context.Times.Where(t => t.ClientID == id));
                context.Invoices.RemoveRange(context.Invoices.Where(t => t.ClientID == id));
                context.Clients.Attach(client);
                context.Clients.Remove(client);
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
        
    }
}
