using System;
using System.Collections.Generic;
using System.Text;
using RobotService.Models.Procedures.Contracts;
using RobotService.Models.Robots.Contracts;
using RobotService.Utilities.Messages;

namespace RobotService.Models.Procedures
{
    public abstract class Procedure : IProcedure
    {
        protected ICollection<IRobot> robots;
        protected Procedure()
        {
            this.robots = new List<IRobot>();
        }

        public ICollection<IRobot> Robots => this.robots;
        public string History()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{this.GetType().Name}");

            foreach (var robot in this.robots)
            {
                sb.AppendLine(robot.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public virtual void DoService(IRobot robot, int procedureTime)
        {
            if (robot.ProcedureTime < procedureTime)
            {
                var message = ExceptionMessages.InsufficientProcedureTime;
                throw new ArgumentException(message);
            }

            robot.ProcedureTime -= procedureTime;
        }
    }
}