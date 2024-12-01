using NUnit.Framework;
using Rad.Mykola.RobotChallange;
using RadMykola.RobotChallenge;
using Robot.Common;
using System.Collections.Generic;

namespace RadMykola.RobotChallenge.Tests
{
    public class RadMykolaAlgorithmTests
    {
        private RadMykolaAlgorithm algorithm;

        [SetUp]
        public void Setup()
        {
            algorithm = new RadMykolaAlgorithm();
        }

        [Test]
        public void TestDoStep_CreatesNewRobot_WhenEnergyIsHighAndLessThan100Robots()
        {
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot { OwnerName = "Rad Mykola", Energy = 250 },
                new Robot.Common.Robot { OwnerName = "Other", Energy = 100 }
            };
            var map = new Map();

            var command = algorithm.DoStep(robots, 0, map);

            Assert.IsInstanceOf<CreateNewRobotCommand>(command);
        }

        [Test]
        public void TestDoStep_CollectsEnergy_WhenNearbyStationHasEnoughEnergy()
        {
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot { OwnerName = "Rad Mykola", Energy = 100, Position = new Position(1, 1) }
            };
            var map = new Map();
            map.Stations.Add(new EnergyStation { Position = new Position(1, 2), Energy = 50 });

            var command = algorithm.DoStep(robots, 0, map);

            Assert.IsInstanceOf<CollectEnergyCommand>(command);
        }

        [Test]
        public void TestDoStep_MovesToNearestFreeCell_WhenStationIsAvailable()
        {
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot { OwnerName = "Rad Mykola", Energy = 100, Position = new Position(1, 1) }
            };
            var map = new Map();
            map.Stations.Add(new EnergyStation { Position = new Position(4, 1), Energy = 50 });

            var expectedFreeCell = new Position(3, 1);

            var command = algorithm.DoStep(robots, 0, map);

            Assert.IsInstanceOf<MoveCommand>(command);
            Assert.AreEqual(expectedFreeCell, ((MoveCommand)command).NewPosition);
        }

        [Test]
        public void TestDoStep_CollectsEnergy_WhenNoStationsAvailable()
        {
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot { OwnerName = "Rad Mykola", Energy = 100, Position = new Position(1, 1) }
            };
            var map = new Map();

            var command = algorithm.DoStep(robots, 0, map);

            Assert.IsInstanceOf<CollectEnergyCommand>(command);
        }
    }
}
