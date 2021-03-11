using EasterRaces.Utilities.Messages;
using System;

namespace EasterRaces.Models.Cars.Entities
{
    public class MuscleCar : Car
    {
        private const double DefaultCubicCentimeters = 5000;
        private const int DefaultMinHorsePower = 400;
        private const int DefaultMaxHorsePower = 600;

        private int horsePower;
        public MuscleCar(string model, int horsePower) 
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
