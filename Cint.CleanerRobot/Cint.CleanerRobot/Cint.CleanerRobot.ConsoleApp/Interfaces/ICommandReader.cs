using Cint.CleanerRobot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cint.CleanerRobot.ConsoleApp
{
    public interface ICommandReader
    {

        int ReadAmountOfCommands();

        Coordinate ReadStartingCoordinate();

        MoveCommand ReadCommand();

        CleanningSession ReadAllCommands();
    }
}
