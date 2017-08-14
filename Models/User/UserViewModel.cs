using System.Collections.Generic;

namespace Econometer.Models.User
{
    public class UserViewModel
    {
           public List<Data.Models.User> Users = new List<Data.Models.User>();
           public List<Data.Models.Account> Accounts = new List<Data.Models.Account>();
    }
}