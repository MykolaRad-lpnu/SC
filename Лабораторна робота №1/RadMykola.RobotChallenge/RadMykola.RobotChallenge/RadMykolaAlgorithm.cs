using RadMykola.RobotChallenge;
using Robot.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Rad.Mykola.RobotChallange
{
    public class RadMykolaAlgorithm : IRobotAlgorithm
    {
        private List<Robot.Common.Robot> myRobots;
        public RadMykolaAlgorithm()
        {
            myRobots = new List<Robot.Common.Robot>();
        }

        public string Author => "Rad Mykola";

        public RobotCommand DoStep(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map)
        {
            myRobots = robots.Where(robot => robot.OwnerName == Author).ToList();

            Robot.Common.Robot robotToMove = robots[robotToMoveIndex];
            if (robotToMove.Energy > 210 && myRobots.Count < 100)
                return new CreateNewRobotCommand();

            var robotPosition = robotToMove.Position;
            var closestStationsList = map.GetNearbyResources(robotPosition, 2);

            if (closestStationsList.Exists(station => station.Energy >= 40))
                return new CollectEnergyCommand();

            var stationsList = map.GetNearbyResources(robotPosition, 99);



            EnergyStation maxEnergyStation = RobotsManager.GetMaxEnergyStation(robots, stationsList, robotToMove);

            if (maxEnergyStation == null)
                return new CollectEnergyCommand();

            var nearestFreeCell = RobotsManager.FindNearestFreeCell(robotPosition, maxEnergyStation.Position, myRobots);

            if (nearestFreeCell != null)
                return new MoveCommand() { NewPosition = nearestFreeCell };
            else
                return new MoveCommand() { NewPosition = maxEnergyStation.Position };
        }
    }
}