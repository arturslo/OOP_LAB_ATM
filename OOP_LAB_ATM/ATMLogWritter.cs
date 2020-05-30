using System.Collections.Generic;
using System.IO;

namespace OOP_LAB_ATM
{
    class ATMLogWritter
    {
        public void Write(List<string> logRows)
        {
            File.WriteAllLines(Path.Combine(Utils.GetProjectDirectoryPath(), "atmLogs.txt"), logRows);
        }
    }
}
