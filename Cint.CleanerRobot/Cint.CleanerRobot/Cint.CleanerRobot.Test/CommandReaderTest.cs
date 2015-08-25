using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Cint.CleanerRobot.Core;
using NUnit.Framework;
using Moq;
using Cint.CleanerRobot.ConsoleApp;

namespace Cint.CleanerRobot.Test
{
    [TestFixture]
    public class CommandReaderTest
    {
        Mock<IView> _mockView;

        [SetUp]
        public void TestInit()
        {
            _mockView = new Mock<IView>();
        }

        [Test]
        public void ReadAmmounOfCommands_integer_input_should_return_integer()
        {
            CommandReader SUT = new CommandReader(_mockView.Object);
            _mockView.Setup(x => x.ReadLine()).Returns("44");
            int result = SUT.ReadAmountOfCommands();

            Assert.AreEqual(44, result);
        }

        [Test]
        public void ReadStartingCoordinate_pair_integer_input_should_return_Coordinate()
        {
            CommandReader SUT = new CommandReader(_mockView.Object);
            _mockView.Setup(x => x.ReadLine()).Returns("2000 3000");
            Coordinate result = SUT.ReadStartingCoordinate();

            Assert.AreEqual(2000, result.X);
            Assert.AreEqual(3000, result.Y);
        }

        [Test]
        public void ReadCommand_text_input_should_return_MoveCommand()
        {
            CommandReader SUT = new CommandReader(_mockView.Object);
            _mockView.Setup(x => x.ReadLine()).Returns("N 333");
            MoveCommand result = SUT.ReadCommand();

            Assert.AreEqual(Direction.N, result.Direction);
            Assert.AreEqual(333, result.Steps);
        }

        [Test]
        public void ReadAllCommand_text_sequence_input_should_return_CleanningSession()
        {
            CommandReader SUT = new CommandReader(_mockView.Object);
            _mockView.SetupSequence(x => x.ReadLine()).Returns("4")
                .Returns("-100 -101")
                .Returns("N 100")
                .Returns("E 1000")
                .Returns("S 500")
                .Returns("W 10");
            CleanningSession result = SUT.ReadAllCommands();

            Assert.AreEqual(-100, result.StartingCoordinate.X);
            Assert.AreEqual(-101, result.StartingCoordinate.Y);
            Assert.AreEqual(4, result.Commands.Count);
            
            Assert.AreEqual(Direction.N, result.Commands[0].Direction);
            Assert.AreEqual(100, result.Commands[0].Steps);

            Assert.AreEqual(Direction.E, result.Commands[1].Direction);
            Assert.AreEqual(1000, result.Commands[1].Steps);

            Assert.AreEqual(Direction.S, result.Commands[2].Direction);
            Assert.AreEqual(500, result.Commands[2].Steps);

            Assert.AreEqual(Direction.W, result.Commands[3].Direction);
            Assert.AreEqual(10, result.Commands[3].Steps);
        }
    }
}
