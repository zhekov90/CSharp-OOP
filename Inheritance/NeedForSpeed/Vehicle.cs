using System;
using System.Collections.Generic;
using System.Text;

namespace NeedForSpeed
{
    public class Vehicle
    {
        private const double DefaultFuelConsumption = 1.25;
        public int HorsePower { get; set; }
        public double Fuel { get; set; }
        public virtual double FuelConsumption => DefaultFuelConsumption;

        public virtual void Drive(double km)
        {
            double fuelAfterDrive = Fuel - km * FuelConsumption;
            if (fuelAfterDrive>=0)
            {
                Fuel = fuelAfterDrive;
            }
        }

        public Vehicle(int horsePower, double fuel)
        {
            HorsePower = horsePower;
            Fuel = fuel;
        }
    }
}
