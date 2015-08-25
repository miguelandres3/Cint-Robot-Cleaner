using Cint.CleanerRobot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cint.CleanerRobot.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IView view = new View();
            ICommandReader reader = new CommandReader(view);
            IRobot robot = new Robot();
            Controller controller = new Controller(view, reader, robot);
            controller.Run();
        }
    }
}
