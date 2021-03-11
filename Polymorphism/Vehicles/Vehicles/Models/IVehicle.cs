namespace Vehicles.Models
{
    public interface IVehicle
    {
        double FuelQuantity { get; }

        double FuelConsumption { get; }

        bool HasAirConditioner { get; }

        double AirConditionerFuelConsumption { get; }

        double TankCapacity { get; }

        bool Drive(double distance);

        void Refuel(double liters);

        void StartAirConditioner();

        void StopAirConditioner();
    }
}
