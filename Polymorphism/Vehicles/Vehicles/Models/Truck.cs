using System;
using Vehicles.Utilities;

namespace Vehicles.Models
{
    public class Truck : Vehicle
    {
        private const double DefaultAirConditionerFuelConsumption = 1.6;
        private const double RefuelPercentage = 0.95;

        public Truck(double fuelQuantity, double fuelConsumption, double tankCapacity, bool hasAirConditioner = true)
            : base(fuelQuantity, fuelConsumption, tankCapacity, hasAirConditioner)
        {
        }

        public override double AirConditionerFuelConsumption => DefaultAirConditionerFuelConsumption;

        public override void Refuel(double liters)
        {
            //this.FuelQuantity += liters * RefuelPercentage;
            if (liters <= 0)
            {
                throw new ArgumentException(ExceptionMessages.NegativeFuelAmount);
            }

            if (this.FuelQuantity + liters > this.TankCapacity)
            {
                string msg = string.Format(ExceptionMessages.InvalidFuelAmount, liters);
                throw new ArgumentException(msg);
            }

            base.Refuel(liters * RefuelPercentage);
        }
    }
}
