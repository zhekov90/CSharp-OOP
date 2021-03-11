using RobotService.Models.Robots.Contracts;

namespace RobotService.Models.Procedures
{
    public class Work : Procedure
    {
        private const int AdditionalHappiness = 12;
        private const int RemovingEnergyPoint = 6;
        public override void DoService(IRobot robot, int procedureTime)
        {
            base.DoService(robot, procedureTime);
            robot.Energy -= RemovingEnergyPoint;
            robot.Happiness += AdditionalHappiness;
        }
    }
}