using Cint.CleanerRobot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cint.CleanerRobot.ConsoleApp
{
    public class CommandReader : ICommandReader
    {
        IView _view;
        public CommandReader(IView view)
        {
            _view = view;
        }
        public int ReadAmountOfCommands()
        {
            return int.Parse(_view.ReadLine());
        }

        public Coordinate ReadStartingCoordinate()
        {
            string line = _view.ReadLine();
            string[] coordinates = line.Split(' ');
            int x = int.Parse(coordinates[0]);
            int y = int.Parse(coordinates[1]);
            return new Core.Coordinate(x, y);
        }

        public MoveCommand ReadCommand()
        {
            string line = _view.ReadLine();
            string[] details = line.Split(' ');
            return new MoveCommand(CoordinateMap.GetDirection(details[0]), int.Parse(details[1]));
        }
        public CleanningSession ReadAllCommands()
        {
            int amountOfCommands = this.ReadAmountOfCommands();
            Coordinate startingCoordinate = this.ReadStartingCoordinate();
            List<MoveCommand> commands = new List<MoveCommand>();
            while (commands.Count < amountOfCommands)
            {
                commands.Add(this.ReadCommand());
            }
            CleanningSession session = new CleanningSession(startingCoordinate, commands);
            return session;
        }
    }
}
