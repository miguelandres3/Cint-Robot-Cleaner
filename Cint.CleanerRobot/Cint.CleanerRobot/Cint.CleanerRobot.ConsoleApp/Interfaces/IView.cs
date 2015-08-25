using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cint.CleanerRobot.ConsoleApp
{
    public interface IView
    {
        string ReadLine();

        void WriteLine(string output);
    }
}
