using System;
using System.Collections.Generic;
using System.Text;

namespace NeedForSpeed
{
    public class RaceMotorcycle : Motorcycle
    {
        private const double DefaultRaceMotorcycleFuelConsumption = 8;
        public RaceMotorcycle(int horsePower, double fuel) : base(horsePower, fuel)
        {
        }

        public override double FuelConsumption => DefaultRaceMotorcycleFuelConsumption;

        public override void Drive(double km)
        {
            double fuelAfterDrive = Fuel - km * FuelConsumption;
            if (fuelAfterDrive>=0)
            {
                Fuel = fuelAfterDrive;
            }
        }
    }
}
