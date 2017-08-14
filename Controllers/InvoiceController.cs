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
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public InvoiceController(ApplicationDbContext _context, IHttpContextAccessor httpContextAccessor) {
            context = _context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Select(int id)
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {
                if (context.Invoices.Any(o => o.ClientID == id))
                {
                    Models.Invoice.InvoiceViewModel viewModel = new Models.Invoice.InvoiceViewModel();
                    viewModel.Clients = await context.Clients.ToListAsync();
                    viewModel.Users = await context.Users.ToListAsync();
                    viewModel.Invoices = await context.Invoices.Where(c => c.ClientID == id).ToListAsync();
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
                var invoice = context.Invoices.Where(i => i.InvoiceID == id).FirstOrDefault<Invoice>();
                return View(invoice);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        
        [HttpPost]
        public IActionResult Delete(Invoice invoiceFromDelete)
        {
            bool isAuthenticated = false;
            if(_session.GetInt32("account") == 1)
            {
            isAuthenticated = true;
            }
            if(isAuthenticated)
            {
                if (invoiceFromDelete != null)
                {
                context.Invoices.Attach(invoiceFromDelete);
                context.Invoices.Remove(invoiceFromDelete);
                context.SaveChanges();
                return RedirectToAction("Select", "Invoice", new { id = invoiceFromDelete.ClientID });
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

    }
}
