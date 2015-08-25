using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cint.CleanerRobot.ConsoleApp
{
    public class View : IView
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string output)
        {
            Console.WriteLine(output);
            Console.ReadLine();
        }
    }
}
