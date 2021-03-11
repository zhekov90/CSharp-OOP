using System;
using RobotService.Models.Robots.Contracts;
using RobotService.Utilities.Messages;

namespace RobotService.Models.Robots
{
    public abstract class Robot : IRobot
    {
        private const int MinValue = 0;
        private const int MaxValue = 100;

        private int _happiness;
        private int _energy;

        protected Robot(string name, int energy, int happiness, int procedureTime)
        {
            this.Name = name;
            this.Happiness = happiness;
            this.Energy = energy;
            this.ProcedureTime = procedureTime;
            this.Owner = "Service";
            this.IsBought = false;
            this.IsChecked = false;
            this.IsChipped = false;
        }
        public string Name { get; }

        public int Happiness
        {
            get => this._happiness;
            set
            {
                if (value < MinValue || value > MaxValue)
                {
                    var message = ExceptionMessages.InvalidHappiness;
                    throw new ArgumentException(message);
                }

                this._happiness = value;
            }
        }

        public int Energy
        {
            get => this._energy;
            set
            {
                if (value < MinValue || value > MaxValue)
                {
                    var message = ExceptionMessages.InvalidEnergy;
                    throw new ArgumentException(message);
                }

                this._energy = value;
            }
        }

        public int ProcedureTime { get; set; }

        public string Owner { get; set; }

        public bool IsBought { get; set; }
        public bool IsChipped { get; set; }
        public bool IsChecked { get; set; }

        public override string ToString()
        {
            return
                string.Format(OutputMessages.RobotInfo, this.GetType().Name, this.Name, this.Happiness, this.Energy);
        }
    }
}