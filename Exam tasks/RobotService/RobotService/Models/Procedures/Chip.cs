using System;
using RobotService.Models.Robots.Contracts;
using RobotService.Utilities.Messages;

namespace RobotService.Models.Procedures
{
    public class Chip : Procedure
    {
        private const int HappinessLosingPoints = 5;
        public override void DoService(IRobot robot, int procedureTime)
        {
            base.DoService(robot, procedureTime);

            robot.Happiness -= HappinessLosingPoints;

            if (robot.IsChipped)
            {
                var message = string.Format(ExceptionMessages.AlreadyChipped, robot.Name);
                throw new ArgumentException(message);
            }

            robot.IsChipped = true;
        }
    }
}