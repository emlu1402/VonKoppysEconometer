using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Econometer.Data.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public DateTime Time { get; set; }
        public string Html { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
        public virtual Client Client { get; set; }
        public virtual User User { get; set; }
        public List<Data.Models.User> Users = new List<Data.Models.User>();
        public List<Data.Models.Client> Clients = new List<Data.Models.Client>();
        public List<Data.Models.Invoice> Invoices = new List<Data.Models.Invoice>();
    }
}