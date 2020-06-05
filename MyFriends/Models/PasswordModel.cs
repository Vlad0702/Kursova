using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFriends.Models
{
    class PasswordModel
    {
        private string Password { get; }

        public PasswordModel(string password)
        {
            Password = password;
        }

        public bool CheckPassword(string password)
        {
            return Password == password;
        }

        
    }
}
