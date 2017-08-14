using System.Collections.Generic;

namespace Econometer.Models.Time
{
    public class TimeViewModel
    {
        public List<Data.Models.Time> Times = new List<Data.Models.Time>();
        public List<Data.Models.Client> Clients = new List<Data.Models.Client>();
        public List<Data.Models.Product> Products = new List<Data.Models.Product>();
        public List<Data.Models.User> Users = new List<Data.Models.User>();

    }
}