using Rad.Mykola.RobotChallange;
using Robot.Common;

namespace RadMykola.RobotChallenge.Test
{
    public class TestRobotsManager
    {
        [TestCase(0, 0, 3, 4, 7)]
        [TestCase(3, 4, 1, 1, 5)]
        [TestCase(0, 0, 0, 0, 0)]
        public void TestGetDistance(int x1, int y1, int x2, int y2, int expectedDistance)
        {
            Position a = new Position(x1, y1);
            Position b = new Position(x2, y2);

            int result = RobotsManager.GetDistance(a, b);

            Assert.AreEqual(expectedDistance, result);
        }

        [TestCase(0, 0, 2, 2)]
        [TestCase(0, 0, 0, 1)]
        [TestCase(0, 0, 0, 0)]

        public void TestIsRobotNearStation_Near(int robotX, int robotY, int stationX, int stationY)
        {
            Position robotPosition = new Position(robotX, robotY);
            Position stationPosition = new Position(stationX, stationY);

            bool result = RobotsManager.IsRobotNearStation(robotPosition, stationPosition);

            Assert.IsTrue(result);
        }

        [TestCase(0, 0, 3, 3)]
        [TestCase(1, 2, 4, 2)]
        public void TestIsRobotNearStation_Far(int robotX, int robotY, int stationX, int stationY)
        {
            Position robotPosition = new Position(robotX, robotY);
            Position stationPosition = new Position(stationX, stationY);

            bool result = RobotsManager.IsRobotNearStation(robotPosition, stationPosition);

            Assert.IsFalse(result);
        }


        [TestCase(2, 2, 4, 5, 3, 3, 4, 4)]
        [TestCase(3, 5, 6, 5, 2, 4, 5, 5)]
        [TestCase(4, 8, 6, 5, 6, 6, 5, 6)]
        public void TestFindNearestFreeCell_ManyFreeCells(int stationX, int stationY, int robotX, int robotY,
                                                              int occupiedX, int occupiedY, int expectedX, int expectedY)
        {
            Position stationPosition = new Position(stationX, stationY);
            Position robotPosition = new Position(robotX, robotY);

            var myRobots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot { Position = new Position(occupiedX - 1, occupiedY + 1) },
                new Robot.Common.Robot { Position = new Position(occupiedX + 1, occupiedY - 1) },
                new Robot.Common.Robot { Position = new Position(occupiedX, occupiedY) }
            };

            Position expectedResult = new Position(expectedX, expectedY);

            Position result = RobotsManager.FindNearestFreeCell(stationPosition, robotPosition, myRobots);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TestFindNearestFreeCell_OneFreeCell()
        {
            Position stationPosition = new Position(2, 2);
            Position robotPosition = new Position(5, 5);
            var myRobots = new List<Robot.Common.Robot>();

            for (int dx = -2; dx <= 2; dx++)
            {
                for (int dy = -2; dy <= 2; dy++)
                {
                    if (dx == 1 && dy == -1)
                        continue;
                    Position takenPosition = new Position
                    {
                        X = stationPosition.X + dx,
                        Y = stationPosition.Y + dy
                    };

                    myRobots.Add(new Robot.Common.Robot() { Position = takenPosition });
                }
            }

            Position expectedResult = new Position(stationPosition.X + 1, stationPosition.Y - 1);

            Position result = RobotsManager.FindNearestFreeCell(stationPosition, robotPosition, myRobots);


            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TestFindNearestFreeCell_NoFreeCells()
        {
            Position stationPosition = new Position(3, 3);
            Position robotPosition = new Position(6, 5);
            var myRobots = new List<Robot.Common.Robot>();
            for (int dx = -2; dx <= 2; dx++)
            {
                for (int dy = -2; dy <= 2; dy++)
                {
                    Position takenPosition = new Position
                    {
                        X = stationPosition.X + dx,
                        Y = stationPosition.Y + dy
                    };

                    myRobots.Add(new Robot.Common.Robot() { Position = takenPosition });
                }
            }

            Position result = RobotsManager.FindNearestFreeCell(stationPosition, robotPosition, myRobots);

            Assert.IsNull(result);
        }

        [TestCase(2, 2, 4, 5, 13)]
        [TestCase(10, 9, 12, 18, 85)]
        public void TestGetEnergyLoss(int stationX, int stationY, int robotX, int robotY, int expectedResult)
        {
            Position stationPosition = new Position(stationX, stationY);
            Position robotPosition = new Position(robotX, robotY);

            int result = RobotsManager.GetEnergyLoss(stationPosition, robotPosition);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TestGetRobotsNearStationCount()
        {
            var stationNearRobotsPosition = new Position(5, 5);
            var stationFarFromRobotsPosition = new Position(0, 0);
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot { Position = new Position { X = 3, Y = 3 } },
                new Robot.Common.Robot { Position = new Position { X = 6, Y = 5 } },
                new Robot.Common.Robot { Position = new Position { X = 7, Y = 5 } }
            };

            int resultThree = RobotsManager.GetRobotsNearStationCount(robots, stationNearRobotsPosition);
            int resultZero = RobotsManager.GetRobotsNearStationCount(robots, stationFarFromRobotsPosition);

            Assert.AreEqual(3, resultThree);
            Assert.AreEqual(0, resultZero);
        }

        [Test]
        public void TestGetMaxEnergyStation_HighestEnergyStation()
        {
            var robots = new List<Robot.Common.Robot>();
            var robot = new Robot.Common.Robot { Position = new Position(2, 2), Energy = 100 };

            var stations = new List<EnergyStation>
            {
                new EnergyStation { Position = new Position(3, 3), Energy = 50, RecoveryRate = 1 },
                new EnergyStation { Position = new Position(4, 4), Energy = 80, RecoveryRate = 2 },
                new EnergyStation { Position = new Position(5, 5), Energy = 20, RecoveryRate = 1 }
            };

            var result = RobotsManager.GetMaxEnergyStation(robots, stations, robot);

            Assert.AreEqual(stations[1], result); 
        }

        [Test]
        public void GetMaxEnergyStation_NoStationWithEnoughEnergy()
        {
            var robots = new List<Robot.Common.Robot>();
            var robot = new Robot.Common.Robot { Position = new Position(2, 2), Energy = 100 };

            var stations = new List<EnergyStation>
            {
                new EnergyStation { Position = new Position(3, 3), Energy = 10, RecoveryRate = 1 },
                new EnergyStation { Position = new Position(4, 4), Energy = 15, RecoveryRate = 1 }
            };

            var result = RobotsManager.GetMaxEnergyStation(robots, stations, robot);

            Assert.IsNull(result);
        }

        [Test]
        public void GetMaxEnergyStation_StationTooFar()
        {
            var robots = new List<Robot.Common.Robot>();
            var robot = new Robot.Common.Robot { Position = new Position(2, 2), Energy = 10 };

            var stations = new List<EnergyStation>
            {
                new EnergyStation { Position = new Position(10, 10), Energy = 80, RecoveryRate = 2 }
            };

            var result = RobotsManager.GetMaxEnergyStation(robots, stations, robot);

            Assert.IsNull(result); 
        }

        [Test]
        public void GetMaxEnergyStation_ConsidersRobotCountAndDistanceToStation()
        {
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot { Position = new Position(5, 5), Energy = 100 },
                new Robot.Common.Robot { Position = new Position(5, 6), Energy = 100 },
                new Robot.Common.Robot { Position = new Position(10, 10), Energy = 100 },  
                new Robot.Common.Robot { Position = new Position(11, 11), Energy = 100 },
                new Robot.Common.Robot { Position = new Position(10, 11), Energy = 100 },
                new Robot.Common.Robot { Position = new Position(12, 12), Energy = 100 }   
            };

            var robot = new Robot.Common.Robot { Position = new Position(2, 2), Energy = 200 };  

            var stations = new List<EnergyStation>
            {
                new EnergyStation { Position = new Position(5, 5), Energy = 100, RecoveryRate = 40 }, 
                new EnergyStation { Position = new Position(10, 10), Energy = 160, RecoveryRate = 50 }  
            };

            var result = RobotsManager.GetMaxEnergyStation(robots, stations, robot);

            Assert.AreEqual(stations[0], result);
        }

        [Test]
        public void TestGetMaxEnergyStation_ConsidersEnergyLoss()
        {
            var robots = new List<Robot.Common.Robot>();
            var robot = new Robot.Common.Robot { Position = new Position(2, 2), Energy = 30 };

            var stations = new List<EnergyStation>
            {
                new EnergyStation { Position = new Position(15, 15), Energy = 500, RecoveryRate = 2 },
                new EnergyStation { Position = new Position(3, 3), Energy = 40, RecoveryRate = 1 }
            };

            var result = RobotsManager.GetMaxEnergyStation(robots, stations, robot);

            Assert.AreEqual(stations[1], result);
        }
    }
}