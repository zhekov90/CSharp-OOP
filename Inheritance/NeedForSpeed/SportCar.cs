using System;
using System.Collections.Generic;
using System.Text;

namespace NeedForSpeed
{
    public class SportCar : Car
    {
        private const double DefaultFuelConsumption = 10;
        public SportCar(int horsePower, double fuel) : base(horsePower, fuel)
        {
        }
        public override double FuelConsumption => DefaultFuelConsumption;

        public override void Drive(double km)
        {
            double fuelAfterDrive = Fuel - km * FuelConsumption;
            if (fuelAfterDrive >= 0)
            {
                Fuel = fuelAfterDrive;
            }
        }
    }
}
