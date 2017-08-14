using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Econometer.Data;
using Econometer.Data.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Econometer.Controllers
{
    public class VatController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public VatController(ApplicationDbContext _context, IHttpContextAccessor httpContextAccessor) {
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
                Models.Vat.VatViewModel viewModel = new Models.Vat.VatViewModel();
                viewModel.Vats = await context.Vats.ToListAsync();

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
                Models.Vat.VatViewModel viewModel = new Models.Vat.VatViewModel();

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public IActionResult Add(Vat vat)
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {   
                context.Vats.Add(vat);
                context.SaveChanges();

                return RedirectToAction("Index", "Vat");
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
                var vat = context.Vats.Where(u => u.VatID == id).FirstOrDefault<Vat>();
                return View(vat);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public IActionResult Edit(Vat vat)
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {   
                if (vat != null)
                {
                context.Vats.Attach(vat);
                var entry = context.Entry(vat);
                context.Entry(vat).State = EntityState.Modified;
                context.SaveChanges();
                }
                return RedirectToAction("Index", "Vat");
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
                var vat = context.Vats.Where(v => v.VatID == id).FirstOrDefault<Vat>();
                return View(vat);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        
        [HttpPost]
        public IActionResult Delete(Vat vatFromDelete)
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {
                if (vatFromDelete != null)
                {
                var vat = context.Vats.Where(v => v.VatID == vatFromDelete.VatID).FirstOrDefault<Vat>();
                vat.Percent = "Borttagen";
                context.SaveChanges();
                }
                return RedirectToAction("Index", "Vat");
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
