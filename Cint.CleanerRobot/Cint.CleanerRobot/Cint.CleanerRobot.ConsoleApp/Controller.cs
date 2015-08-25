using Cint.CleanerRobot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cint.CleanerRobot.ConsoleApp
{
    public class Controller
    {
        IView _view;
        ICommandReader _reader;
        IRobot _robot;
        public Controller(IView view, ICommandReader reader, IRobot robot)
        {
            _view = view;
            _reader = reader;
            _robot = robot;
        }

        public void Run()
        {
            CleanningSession session = _reader.ReadAllCommands();
            int placesClean = _robot.ExecuteClean(session);
            _view.WriteLine(string.Format(Resources.ResultLabel, placesClean));
        }

    }
}
