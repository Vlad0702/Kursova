using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFriends.Models
{
    class PhoneOperator
    {
        public PhoneOperator(string name, List<int> CodesOfNumber)
        {
            Name = name;
            this.CodesOfNumber = CodesOfNumber;
        }

        public PhoneOperator() { }

        public string Name { get; }
        public List<int> CodesOfNumber { get; }

        public bool IsCodeContains(int code)
        {
            return CodesOfNumber.Contains(code);
        } 
    }
}
