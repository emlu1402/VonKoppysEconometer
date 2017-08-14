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
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public ProductController(ApplicationDbContext _context, IHttpContextAccessor httpContextAccessor) {
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
                Models.Product.ProductViewModel viewModel = new Models.Product.ProductViewModel();
                viewModel.Products = await context.Products.ToListAsync();
                viewModel.Vats = await context.Vats.ToListAsync();

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
                Models.Product.ProductViewModel viewModel = new Models.Product.ProductViewModel();
                viewModel.Vats = await context.Vats.ToListAsync();

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {   
                context.Products.Add(product);
                context.SaveChanges();

                return RedirectToAction("Index", "Product");
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
                var product = context.Products.Where(p => p.ProductID == id).FirstOrDefault<Product>();
                product.Vats = context.Vats.ToList();
                return View(product);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {   
                if (product != null)
                {
                context.Products.Attach(product);
                var entry = context.Entry(product);
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
                }
                return RedirectToAction("Index", "Product");
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
                var product = context.Products.Where(p => p.ProductID == id).FirstOrDefault<Product>();
                return View(product);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        
        [HttpPost]
        public IActionResult Delete(Product productFromDelete)
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {
                if (productFromDelete != null)
                {
                var product = context.Products.Where(p => p.ProductID == productFromDelete.ProductID).FirstOrDefault<Product>();
                product.Name = "Borttagen";
                product.VatID = 5;
                context.SaveChanges();
                }
                return RedirectToAction("Index", "Product");
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
