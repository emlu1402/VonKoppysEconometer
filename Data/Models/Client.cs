using System.Collections.Generic;

namespace Econometer.Data.Models
{
    public class Client
    {
        public int ClientID { get; set; }
        public string Orgnumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string Poc { get; set; }
        public virtual ICollection<Time> Times { get; set; }
    }
}