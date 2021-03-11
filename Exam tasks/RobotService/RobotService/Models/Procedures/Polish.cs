using RobotService.Models.Robots.Contracts;

namespace RobotService.Models.Procedures
{
    public class Polish : Procedure
    {
        private const int RemovingHappinessPoints = 7;
        public override void DoService(IRobot robot, int procedureTime)
        {
            base.DoService(robot, procedureTime);
            robot.Happiness -= RemovingHappinessPoints;
        }
    }
}