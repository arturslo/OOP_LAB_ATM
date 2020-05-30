using System;

namespace OOP_LAB_ATM
{
    public class ATMConfiguredArgs : EventArgs
    {
        public DateTime Date { get; }
        public int Change { get; }
        public ATMConfiguredArgs(DateTime date, int change)
        {
            this.Date = date;
            this.Change = change;
        }
    }
}
