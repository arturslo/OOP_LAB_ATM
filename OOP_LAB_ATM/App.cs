using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace OOP_LAB_ATM
{
    class App
    {
        private IATM atm;
        private List<IUserAccount> accounts;
        private IUserAccount selectedAccount;

        public App()
        {
            var jsonString = File.ReadAllText(Path.Combine(Utils.GetProjectDirectoryPath(), "data.json"));
            var appData = JsonSerializer.Deserialize<AppData>(jsonString);
            atm = new ATM(appData.ATMData.Balance, appData.ATMData.ConfigurationPassword, new ATMLogWritter());
            accounts = new List<IUserAccount>(appData.Accounts);
            selectedAccount = null;
        }

        public void Run()
        {
            string userInput = "";
            do
            {
                Console.WriteLine();
                PrintOptions();
                Console.WriteLine();
                PrintSelectedAccount();
                Console.WriteLine();
                Console.Write("input option: ");
                userInput = Console.ReadLine();
                Console.WriteLine();

                if (userInput.Equals(Option.DISPLAY_ATM))
                {
                    Console.WriteLine("ATM info:");
                    atm.Print();
                }
                else if (userInput.Equals(Option.DISPLAY_ACCOUNTS))
                {
                    Console.WriteLine("accounts info:");
                    foreach (var account in accounts)
                    {
                        account.Print();
                        Console.WriteLine();
                    }
                }
                else if (userInput.Equals(Option.SELECT_ACCOUNT))
                {
                    Console.WriteLine("select account");
                    Console.Write("account number: ");
                    string accountNumberInput = Console.ReadLine();
                    var foundAccount = accounts.Find(account => account.AccountNumber.Equals(accountNumberInput));

                    if (foundAccount == null)
                    {
                        Console.WriteLine("account not found");
                        continue;
                    }

                    Console.WriteLine("account selected");
                    selectedAccount = foundAccount;

                }
                else if (userInput.Equals(Option.WITHDRAW_MONEY))
                {
                    if (selectedAccount == null)
                    {
                        Console.WriteLine("no account selected");
                        continue;
                    }

                    Console.Write("enter password: ");
                    var passwordInput = Console.ReadLine();

                    if (!selectedAccount.CheckPassword(passwordInput))
                    {
                        Console.WriteLine("wrong password");
                        continue;
                    }

                    atm.CheckAccountBalance(selectedAccount);
                    Console.WriteLine();
                    Console.Write("withdraw amount: ");
                    var withdrawAmountInput = Console.ReadLine();
                    int withdrawAmount;
                    var isParsed = int.TryParse(withdrawAmountInput, out withdrawAmount);

                    if (!isParsed)
                    {
                        Console.WriteLine("not integer");
                        continue;
                    }

                    var oldAccountBalance = selectedAccount.Balance;
                    atm.WithdrawMoney(selectedAccount, withdrawAmount);
                    var resultText = "money withdrawn";

                    if (oldAccountBalance == selectedAccount.Balance)
                    {
                        resultText = "no changes";
                    }

                    Console.WriteLine(resultText);
                }
                else if (userInput.Equals(Option.CONFIGURE_ATM))
                {
                    Console.Write("enter configuration password: ");
                    var configurationPasswordInput = Console.ReadLine();

                    if (!atm.CheckConfigurationPassword(configurationPasswordInput))
                    {
                        Console.WriteLine("wrong password");
                        continue;
                    }

                    Console.Write("set ATM balance: ");
                    var newBalanceInput = Console.ReadLine();
                    int newBalance;
                    var isParsed = int.TryParse(newBalanceInput, out newBalance);

                    if (!isParsed)
                    {
                        Console.WriteLine("Not valid integer");
                        continue;
                    }

                    var oldAtmBalance = atm.Balance;
                    atm.SetBalance(newBalance);

                    var resultText = "balance changed";
                    if (oldAtmBalance == atm.Balance)
                    {
                        resultText = "no changes";
                    }

                    Console.WriteLine(resultText);
                }
                else if (userInput.Equals(Option.WRITE_LOG))
                {
                    atm.WriteLog();
                    Console.WriteLine("log written");
                }
            } while (!userInput.Equals(Option.EXIT));
        }

        private void PrintOptions()
        {
            var text = String.Join(
                Environment.NewLine,
                "1 - display atm",
                "2 - display accounts",
                "3 - select account",
                "4 - withdraw money",
                "5 - configure ATM",
                "6 - write logs",
                "7 - exit"
            );

            Console.WriteLine(text);
        }

        private void PrintSelectedAccount()
        {
            if (selectedAccount == null)
            {
                return;
            }

            Console.WriteLine($"selected account: {selectedAccount.AccountNumber}");
        }
    }
}
