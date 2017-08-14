using System.Collections.Generic;

namespace Econometer.Data.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int AccountID { get; set; }
        public virtual ICollection<Time> Times { get; set; }
        public virtual Account Account { get; set; }
        public List<Data.Models.Account> Accounts = new List<Data.Models.Account>();
    }
}