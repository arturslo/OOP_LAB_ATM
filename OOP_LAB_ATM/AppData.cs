using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LAB_ATM
{
    class ATMData
    {
        public int Balance { get; set; }
        public string ConfigurationPassword { get; set; }
    }
    class AppData
    {
        public ATMData ATMData { get; set; }
        public IList<UserAccount> Accounts { get; set; }
    }
}
