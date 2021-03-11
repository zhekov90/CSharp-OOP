using RobotService.Models.Robots.Contracts;

namespace RobotService.Models.Procedures
{
    public class TechCheck : Procedure
    {
        private const int EnergyLossPoints = 8;
        public override void DoService(IRobot robot, int procedureTime)
        {
            base.DoService(robot, procedureTime);

            robot.Energy -= EnergyLossPoints;

            if (robot.IsChecked)
            {
                robot.Energy -= EnergyLossPoints;
            }

            robot.IsChecked = true;
        }
    }
}