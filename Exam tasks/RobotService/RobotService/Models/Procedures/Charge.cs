using RobotService.Models.Robots.Contracts;

namespace RobotService.Models.Procedures
{
    public class Charge : Procedure
    {
        private const int AdditionalHappiness = 12;
        private const int AdditionalEnergy = 10;
        public override void DoService(IRobot robot, int procedureTime)
        {
            base.DoService(robot, procedureTime);
            robot.Happiness += AdditionalHappiness;
            robot.Energy += AdditionalEnergy;
        }
    }
}
