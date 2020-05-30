using System.IO;

namespace OOP_LAB_ATM
{
    static class Utils
    {
        public static string GetProjectDirectoryPath()
        {
            return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        }
    }
}
