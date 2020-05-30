using System;

namespace OOP_LAB_ATM
{
    class UserAccount : IUserAccount
    {
        private int _balance;
        public string FullName { get; set; }
        public string AccountNumber { get; set; }
        public string CardPassword { get; set; }
        public int Balance
        {
            get => _balance;
            set
            {
                if (value > 0) { _balance = value; }
            }
        }

        public UserAccount()
        { 
        }

        public UserAccount(string fullName, string accountNumber, string cardPassword, int balance)
        {
            FullName = fullName;
            AccountNumber = accountNumber;
            CardPassword = cardPassword;
            Balance = balance;
        }

        public bool CheckPassword(string password)
        {
            return CardPassword.Equals(password);
        }

        public void Print()
        {
            Console.WriteLine($"fullName: {FullName}");
            Console.WriteLine($"accountNumber: {AccountNumber}");
            Console.WriteLine($"cardPassword: {CardPassword}");
            Console.WriteLine($"balance: {Balance}");
        }
    }
}
