using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Cint.CleanerRobot.Core;
using NUnit.Framework;
using Moq;

namespace Cint.CleanerRobot.Test
{
    [TestFixture]
    public class RobotTest
    {
        [SetUp]
        public void TestInit()
        {
        }


        [Test]
        public void JumpTo_position_should_change_CurrentPosition()
        {
            Robot SUT = new Robot();
            SUT.JumpTo(new Coordinate(-2000, 3000));

            Assert.AreEqual(-2000, SUT.CurrentPosition.X);
            Assert.AreEqual(3000, SUT.CurrentPosition.Y);
        }

        [Test]
        public void MoveTowards_direction_E_should_change_CurrentPosition()
        {
            Robot SUT = new Robot();
            SUT.JumpTo(new Coordinate(0, 0));

            SUT.MoveTowards(Direction.E, 2000);

            Assert.AreEqual(2000, SUT.CurrentPosition.X);
            Assert.AreEqual(0, SUT.CurrentPosition.Y);
        }

        [Test]
        public void MoveTowards_direction_W_should_change_CurrentPosition()
        {
            Robot SUT = new Robot();
            SUT.JumpTo(new Coordinate(0, 0));
            SUT.MoveTowards(Direction.W, 2000);

            Assert.AreEqual(-2000, SUT.CurrentPosition.X);
            Assert.AreEqual(0, SUT.CurrentPosition.Y);
        }

        [Test]
        public void MoveTowards_direction_N_should_change_CurrentPosition()
        {
            Robot SUT = new Robot();
            SUT.JumpTo(new Coordinate(0, 0));
            SUT.MoveTowards(Direction.N, 2000);

            Assert.AreEqual(0, SUT.CurrentPosition.X);
            Assert.AreEqual(2000, SUT.CurrentPosition.Y);
        }

        [Test]
        public void MoveTowards_direction_S_should_change_CurrentPosition()
        {
            Robot SUT = new Robot();
            SUT.JumpTo(new Coordinate(0, 0));
            SUT.MoveTowards(Direction.S, 2000);

            Assert.AreEqual(0, SUT.CurrentPosition.X);
            Assert.AreEqual(-2000, SUT.CurrentPosition.Y);
        }

        [Test]
        public void ExecuteClean_Session_One_Line_should_return_4001()
        {
            List<MoveCommand> commands = new List<MoveCommand>();
            commands.Add(new MoveCommand(Direction.E, 4000));
            CleanningSession session = new CleanningSession(new Coordinate(-2000, 2000), commands);

            Robot SUT = new Robot();
            int places = SUT.ExecuteClean(session);

            Assert.AreEqual(2000, SUT.CurrentPosition.X);
            Assert.AreEqual(2000, SUT.CurrentPosition.Y);
            Assert.AreEqual(4001, places);
        }

        [Test]
        public void ExecuteClean_Session_Square_should_return_800000()
        {
            Robot SUT = new Robot();

            List<MoveCommand> commands = new List<MoveCommand>();
            commands.Add(new MoveCommand(Direction.E, 200000));
            commands.Add(new MoveCommand(Direction.S, 200000));
            commands.Add(new MoveCommand(Direction.W, 200000));
            commands.Add(new MoveCommand(Direction.N, 200000));
            CleanningSession session = new CleanningSession(new Coordinate(-100000, 100000), commands);
            int places = SUT.ExecuteClean(session);

            Assert.AreEqual(-100000, SUT.CurrentPosition.X);
            Assert.AreEqual(100000, SUT.CurrentPosition.Y);
            Assert.AreEqual(800000, places);
        }

        [Test]
        public void ExecuteClean_Session_Repeating_line_should_return_3001()
        {
            Robot SUT = new Robot();

            List<MoveCommand> commands = new List<MoveCommand>();
            commands.Add(new MoveCommand(Direction.S, 3000));
            commands.Add(new MoveCommand(Direction.N, 3000));
            CleanningSession session = new CleanningSession(new Coordinate(-20000, -20000), commands);
            int places = SUT.ExecuteClean(session);

            Assert.AreEqual(-20000, SUT.CurrentPosition.X);
            Assert.AreEqual(-20000, SUT.CurrentPosition.Y);
            Assert.AreEqual(3001, places);
        }

        [Test]
        public void ExecuteClean_Session_3_Intersections_should_avoid_repeated()
        {
            Robot SUT = new Robot();

            List<MoveCommand> commands = new List<MoveCommand>();
            commands.Add(new MoveCommand(Direction.E, 100));
            commands.Add(new MoveCommand(Direction.S, 20));
            commands.Add(new MoveCommand(Direction.W, 20));
            commands.Add(new MoveCommand(Direction.N, 24));
            commands.Add(new MoveCommand(Direction.E, 10));
            commands.Add(new MoveCommand(Direction.S, 30));
            CleanningSession session = new CleanningSession(new Coordinate(0, 0), commands);
            int places = SUT.ExecuteClean(session);

            Assert.AreEqual(100 - 20 + 10, SUT.CurrentPosition.X);
            Assert.AreEqual(-20 + 24 - 30, SUT.CurrentPosition.Y);
            Assert.AreEqual(1 + 100 + 20 + 20 + 24 + 10 + 30 - 3, places);//Expected: initial place + total steps - intersections
        }
    }
}
