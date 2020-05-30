using System;
using System.Collections.Generic;

namespace OOP_LAB_ATM
{
    class ATM : IATM
    {
        private int _balance;
        private string configurationPassword;
        private List<string> logRows;
        private ATMLogWritter atmLogWritter;

        public delegate void MoneyWithdrawnEventHandler(object source, MoneyWithdrawnArgs eventArgs);
        public event MoneyWithdrawnEventHandler MoneyWithdrawnEvent;

        public delegate void ATMConfigureEventHandler(object source, ATMConfiguredArgs eventArgs);
        public event ATMConfigureEventHandler ATMConfiguredEvent;

        public int Balance
        {
            get => _balance;
            set
            {
                if (value > 0) { _balance = value; }
            }
        }

        public ATM(int balance, string configurationPassword, ATMLogWritter atmLogWritter)
        {
            Balance = balance;
            this.configurationPassword = configurationPassword;
            this.logRows = new List<string>();
            this.atmLogWritter = atmLogWritter;
            MoneyWithdrawnEvent += LogMoneyWithdrawnEvent;
            ATMConfiguredEvent += LogATMConfiguredEvent;
        }

        public void CheckAccountBalance(IUserAccount account)
        {
            Console.WriteLine($"account balance: {account.Balance}");
        }

        public void SetBalance(int newBalance)
        {
            if (newBalance < 0)
            {
                return;
            }

            var change = Math.Abs(Balance - newBalance);
            if (newBalance < Balance)
            {
                change = -change;
            }

            Balance = newBalance;
            RaiseATMConfiguredEvent(change);
        }

        public void WithdrawMoney(IUserAccount account, int amount)
        {
            if (amount <= 0)
            {
                return;
            }
            if (account.Balance - amount <= 0)
            {
                return;
            }
            if (Balance - amount <= 0)
            {
                return;
            }

            account.Balance -= amount;
            Balance -= amount;

            RaiseMoneyWithdrawnEvent(amount, account);
        }

        public void WriteLog()
        {
            atmLogWritter.Write(logRows);
        }

        public void Print()
        {
            Console.WriteLine($"balance: {Balance}");
            Console.WriteLine($"configurationPassword: {configurationPassword}");
        }

        public bool CheckConfigurationPassword(string password)
        {
            return configurationPassword.Equals(password);
        }

        protected virtual void RaiseMoneyWithdrawnEvent(int withdrawnAmount, IUserAccount account)
        {
            MoneyWithdrawnEvent?.Invoke(this, new MoneyWithdrawnArgs(withdrawnAmount, account));
        }

        protected virtual void RaiseATMConfiguredEvent(int change)
        {
            ATMConfiguredEvent?.Invoke(this, new ATMConfiguredArgs(new DateTime(), change));
        }

        private void LogMoneyWithdrawnEvent(object source, MoneyWithdrawnArgs eventArgs)
        {
            logRows.Add($"withdrawn: {eventArgs.WithdrawnAmount} account: {eventArgs.Account.AccountNumber}");
        }

        private void LogATMConfiguredEvent(object source, ATMConfiguredArgs eventArgs)
        {
            logRows.Add($"date: {eventArgs.Date} change: {eventArgs.Change}");
        }
    }
}
