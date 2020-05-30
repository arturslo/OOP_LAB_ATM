namespace OOP_LAB_ATM
{
    public interface IATM : IPrintable
    {
        void WithdrawMoney(IUserAccount account, int amount);
        void WriteLog();
        void CheckAccountBalance(IUserAccount account);
        void SetBalance(int newBalance);
        bool CheckConfigurationPassword(string password);
        int Balance { get; }
    }
}
