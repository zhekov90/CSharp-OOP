using System;
using System.Collections.Generic;
using System.Linq;
using RobotService.Models.Garages.Contracts;
using RobotService.Models.Robots.Contracts;
using RobotService.Utilities.Messages;

namespace RobotService.Models.Garages
{
    public class Garage : IGarage
    {
        private const int CapacityValue = 10;

        private readonly Dictionary<string, IRobot> _robots;
        public Garage()
        {
            this._robots = new Dictionary<string, IRobot>();
        }
        public int Capacity => CapacityValue;
        public IReadOnlyDictionary<string, IRobot> Robots => this._robots;
        public void Manufacture(IRobot robot)
        {
            if (this._robots.Count + 1 > this.Capacity)
            {
                var message = ExceptionMessages.NotEnoughCapacity;
                throw new InvalidOperationException(message);
            }

            if (this._robots.ContainsKey(robot.Name))
            {
                var message = string.Format(ExceptionMessages.ExistingRobot, robot.Name);
                throw new ArgumentException(message);
            }

            this._robots.Add(robot.Name, robot);
        }

        public void Sell(string robotName, string ownerName)
        {
            if (!this._robots.ContainsKey(robotName))
            {
                var message = string.Format(ExceptionMessages.InexistingRobot, robotName);
                throw new ArgumentException(message);
            }

            var robot = this._robots.First(r => r.Key == robotName).Value;

            robot.Owner = ownerName;
            robot.IsBought = true;
            this._robots.Remove(robot.Name);
        }
    }
}