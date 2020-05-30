namespace OOP_LAB_ATM
{
    public interface IUserAccount : IPrintable
    {
        bool CheckPassword(string password);
        string FullName { get; set; }
        string AccountNumber { get; set; }
        string CardPassword { get; set; }
        int Balance { get; set; }
    }
}
