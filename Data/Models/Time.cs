using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Econometer.Data.Models
{
    public class Time
    {
        public int TimeID { get; set; }
        public DateTime Starttime { get; set; }
        public Nullable<DateTime> Endtime { get; set; }
        public string Description { get; set; }
        public Nullable<double> Hours { get; set; }
        public bool Invoiced { get; set; }
        public int ProductID { get; set; }
        public int ClientID { get; set; }
        public virtual Product Product { get; set; }
        public int UserID { get; set; }
        public virtual Client Client { get; set; }
        public virtual User User { get; set; }
        public List<Data.Models.User> Users = new List<Data.Models.User>();
        public List<Data.Models.Product> Products = new List<Data.Models.Product>();
    }
}