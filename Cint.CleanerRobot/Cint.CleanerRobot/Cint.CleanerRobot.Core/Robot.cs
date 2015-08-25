using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cint.CleanerRobot.Core
{
    public class Robot : IRobot
    {
        private Coordinate _currentPosition;
        private Dictionary<Coordinate, bool> _cleanOffices;

        public Coordinate CurrentPosition
        {
            get { return _currentPosition; }
            private set
            {
                _currentPosition = value;
                CleanCurrentPosition();
            }
        }

        public Robot()
        {
            _cleanOffices = new Dictionary<Coordinate, bool>();
        }

        public void JumpTo(Coordinate position)
        {
            CurrentPosition = position;
        }

        private void CleanCurrentPosition()
        {
            _cleanOffices[_currentPosition] = true;
        }

        public void MoveTowards(Direction direction, int steps)
        {
            Coordinate directionStep = CoordinateMap.GetDirectionStep(direction);
            for (int i = 0; i < steps; i++)
            {
                CurrentPosition = new Coordinate(_currentPosition.X + directionStep.X, _currentPosition.Y + directionStep.Y);
            }
        }

        public int ExecuteClean(CleanningSession session)
        {
            JumpTo(session.StartingCoordinate);
            foreach (var command in session.Commands)
            {
                this.MoveTowards(command.Direction, command.Steps);
            }
            return _cleanOffices.Count;
        }
    }
}
