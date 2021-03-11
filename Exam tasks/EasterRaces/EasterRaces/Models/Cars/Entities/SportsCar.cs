using EasterRaces.Utilities.Messages;
using System;

namespace EasterRaces.Models.Cars.Entities
{
    public class SportsCar : Car
    {
        private const double DefaultCubicCentimeters = 3000;
        private const int DefaultMinHorsePower = 250;
        private const int DefaultMaxHorsePower = 450;

        private int horsePower;
        public SportsCar(string model, int horsePower)
            : base(model, horsePower, DefaultCubicCentimeters, DefaultMinHorsePower, DefaultMaxHorsePower)
        {
        }

        public override int HorsePower
        {
            get
            {
                return this.horsePower;
            }
            protected set
            {
                if (value < DefaultMinHorsePower || value > DefaultMaxHorsePower)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidHorsePower, value));
                }
                this.horsePower = value;
            }
        }
    }
}
