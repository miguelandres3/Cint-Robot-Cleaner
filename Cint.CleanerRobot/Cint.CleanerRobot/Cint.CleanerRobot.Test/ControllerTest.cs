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
    public class ControllerTest
    {
        Mock<IView> _mockView;
        Mock<ICommandReader> _mockReader;
        Mock<IRobot> _mockRobot;

        [SetUp]
        public void TestInit()
        {
            _mockView = new Mock<IView>();
            _mockReader = new Mock<ICommandReader>();
            _mockRobot = new Mock<IRobot>();
        }

        [Test]
        public void Run_should_display_places_cleaned()
        {
            Controller SUT = new Controller(_mockView.Object, _mockReader.Object, _mockRobot.Object);
            _mockReader.Setup(x => x.ReadAllCommands()).Returns(new CleanningSession(
                new Coordinate(0,0), new List<MoveCommand>()));

            _mockRobot.Setup(x => x.ExecuteClean(It.IsAny<CleanningSession>())).Returns(1001);
            _mockView.Setup(x => x.WriteLine(It.IsAny<String>()));

            SUT.Run();

            _mockView.Verify(w => w.WriteLine(It.Is<string>(s => s == "=>Cleaned 1001")), Times.Once);
        }

    }
}
