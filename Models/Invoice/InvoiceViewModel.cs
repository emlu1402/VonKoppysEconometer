using System.Collections.Generic;

namespace Econometer.Models.Invoice
{
    public class InvoiceViewModel
    {
        public List<Data.Models.Client> Clients = new List<Data.Models.Client>();
        public List<Data.Models.Invoice> Invoices = new List<Data.Models.Invoice>();
        public List<Data.Models.User> Users = new List<Data.Models.User>();

    }
}