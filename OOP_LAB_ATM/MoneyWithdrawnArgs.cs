using System;

namespace OOP_LAB_ATM
{
    public class MoneyWithdrawnArgs : EventArgs
    {
        public int WithdrawnAmount { get; }
        public IUserAccount Account { get; }
        public MoneyWithdrawnArgs(int withdrawnAmount, IUserAccount account)
        {
            this.WithdrawnAmount = withdrawnAmount;
            this.Account = account;
        }
    }
}
