using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace MyFriends.Data
{
    [Serializable]
    public class Friend
    {
        public Friend(int id, string name, string surname, string patronymic, int year, int month, int day, string address, int numberCode, int number)
        {
            Name = name.Trim();
            Surname = surname.Trim();
            Patronymic = patronymic.Trim();
            Id = id;
            Year = year;
            Month = month;
            Day = day;
            Address = address.Trim();
            NumberCode = numberCode;
            Number = number;
        }

        public Friend(string name, string surname, string patronymic, int year, int month, int day, string address, int numberCode, int number)
        {
            Name = name.Trim();
            Surname = surname.Trim();
            Patronymic = patronymic.Trim();
            Year = year;
            Month = month;
            Day = day;
            Address = address.Trim();
            NumberCode = numberCode;
            Number = number;
        }


        public Friend() { }

        [DataMember(Name = "Name")]
        public string Name { get; set; }
        [DataMember(Name = "Surname")]
        public string Surname { get; set; }
        [DataMember(Name = "Patronymic")]
        public string Patronymic { get; set; }
        [DataMember(Name = "Id")]
        public int Id { get; set;  }
        [DataMember(Name = "Year")]
        public int Year { get; set; }
        [DataMember(Name = "Month")]
        public int Month { get; set; }
        [DataMember(Name = "Day")]
        public int Day { get; set; }
        [DataMember(Name = "Address")]
        public string Address { get; set; }
        [DataMember(Name = "NumberKey")]
        public int NumberCode { get; set; }
        [DataMember(Name = "Number")]
        public int Number { get; set; }
        public string PhoneNumber {
            get { return Number ==0 ? "" : ("(" + NumberCodeString + ")" + NumberString); }
        }
        public string NumberCodeString
        {
            get
            {
                if (NumberCode.ToString().Length == 2)
                {
                    return $"0{NumberCode}";
                }

                if (NumberCode.ToString().Length == 1)
                {
                    return $"0{NumberCode}";
                }

                return Number.ToString();
            }
        }
        public string DateofBirth
        {
            get { return Day + "." + Month + "." + Year; }
        }
        public string FullName
        {
            get { return Name + " " + Surname + " " + Patronymic; }
        }
        public string NumberString
        {
            get
            {
                if (Number.ToString().Length == 7)
                {
                    Number.ToString();
                }
                string numberString = Number.ToString();
                while (numberString.Length < 7)
                {
                    numberString = "0" + numberString;
                }

                return numberString;
            }
        }


    }

    
}
