using Robot.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RadMykola.RobotChallenge
{
    public static class RobotsManager
    {
        private const int ENERGY_PER_COLLECTION = 40;

        public static bool IsRobotNearStation(Position robotPosition, Position stationPosition) =>
            Math.Abs(robotPosition.X - stationPosition.X) <= 2 && Math.Abs(robotPosition.Y - stationPosition.Y) <= 2;

        public static int GetDistance(Position a, Position b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        public static Position FindNearestFreeCell(Position stationPosition, Position robotPosition, List<Robot.Common.Robot> myRobots)
        {
            Position nearestFreeCell = null;
            int minDistanceToRobot = int.MaxValue;

            for (int dx = -2; dx <= 2; dx++)
            {
                for (int dy = -2; dy <= 2; dy++)
                {
                    Position candidatePosition = new Position
                    {
                        X = stationPosition.X + dx,
                        Y = stationPosition.Y + dy
                    };

                    if (IsCellFree(candidatePosition, myRobots))
                    {
                        int distanceToRobot = GetDistance(robotPosition, candidatePosition);

                        if (distanceToRobot < minDistanceToRobot)
                        {
                            minDistanceToRobot = distanceToRobot;
                            nearestFreeCell = candidatePosition;
                        }
                    }
                }
            }

            return nearestFreeCell;
        }

        public static bool IsCellFree(Position cellPosition, List<Robot.Common.Robot> myRobots) => 
            !myRobots.Exists(robot => robot.Position == cellPosition);

        public static int GetEnergyLoss(Position a, Position b)
        {
            return (int)(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        public static int GetRobotsNearStationCount(IList<Robot.Common.Robot> robots, Position stationPosition)
        {
            return robots.Count(robot => IsRobotNearStation(robot.Position, stationPosition));
        }

        public static int GetFreeEnergyOnStation(EnergyStation station, IList<Robot.Common.Robot> robots)
        {
            return (station.Energy + station.RecoveryRate) -
                GetRobotsNearStationCount(robots, station.Position) * ENERGY_PER_COLLECTION;
        }

        public static EnergyStation GetMaxEnergyStation(IList<Robot.Common.Robot> robots, 
            List<EnergyStation> stationList, Robot.Common.Robot robot)
        {
            var availableStations = stationList
                .Where(station => GetEnergyLoss(robot.Position, station.Position) < (robot.Energy - 10) 
                && GetFreeEnergyOnStation(station, robots) >= ENERGY_PER_COLLECTION)
                .OrderByDescending(station => GetFreeEnergyOnStation(station, robots))
                .ToList();

            return availableStations.FirstOrDefault();
        }
    }
}
