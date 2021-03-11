using RobotService.Models.Robots.Contracts;

namespace RobotService.Models.Procedures
{
    public class Rest : Procedure
    {
        private const int RemovingHappinessPoints = 3;
        private const int AdditionalEnergy = 10;

        public override void DoService(IRobot robot, int procedureTime)
        {
            base.DoService(robot, procedureTime);
            robot.Happiness -= RemovingHappinessPoints;
            robot.Energy += AdditionalEnergy;
        }
    }
}