using System.Collections.Generic;

namespace Econometer.Data.Models
{
    public class Account
    {
        public int AccountID { get; set; }
        public string Type { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}