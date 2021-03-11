using System;
using Vehicles.Factories;
using Vehicles.IO;
using Vehicles.Models;

namespace Vehicles.Core
{
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IVehicleFactory vehicleFactory;

        public Engine(IReader reader, IWriter writer, IVehicleFactory vehicleFactory)
        {
            this.reader = reader;
            this.writer = writer;
            this.vehicleFactory = vehicleFactory;
        }

        public void Run()
        {
            string[] carData = this.reader.CustomReadLine().Split();
            IVehicle car = CreateVehicle(carData);

            string[] truckData = this.reader.CustomReadLine().Split();
            IVehicle truck = CreateVehicle(truckData);

            string[] busData = this.reader.CustomReadLine().Split();
            IVehicle bus = CreateVehicle(busData);

            int n = int.Parse(this.reader.CustomReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] args = this.reader.CustomReadLine().Split();
                string command = args[0];
                string vehicleType = args[1];
                double arg = double.Parse(args[2]);

                try
                {
                    if (command == "Drive")
                    {
                        this.DriveCommand(vehicleType, car, truck, bus, arg);
                    }
                    else if (command == "DriveEmpty")
                    {
                        this.DriveEmptyCommand(bus, arg);
                    }
                    else if (command == "Refuel")
                    {
                        this.RefuelCommand(vehicleType, car, truck, bus, arg);
                    }
                }
                catch (ArgumentException e)
                {
                    this.writer.CustomWriteLine(e.Message);
                }
            }

            this.writer.CustomWriteLine(car.ToString());
            this.writer.CustomWriteLine(truck.ToString());
            this.writer.CustomWriteLine(bus.ToString());
        }

        private void RefuelCommand(string vehicleType, IVehicle car, IVehicle truck, IVehicle bus, double arg)
        {
            if (vehicleType == nameof(Car))
            {
                car.Refuel(arg);
            }
            else if (vehicleType == nameof(Truck))
            {
                truck.Refuel(arg);
            }
            else if (vehicleType == nameof(Bus))
            {
                bus.Refuel(arg);
            }
        }

        private IVehicle CreateVehicle(string[] data)
        {
            string type = data[0];
            double fuelQuantity = double.Parse(data[1]);
            double fuelConsumption = double.Parse(data[2]);
            double tankCapacity = double.Parse(data[3]);

            IVehicle vehicle = this.vehicleFactory.CreateVehicle(type, fuelQuantity, fuelConsumption, tankCapacity);
            return vehicle;
        }

        private void DriveCommand(string vehicleType, IVehicle car, IVehicle truck, IVehicle bus, double arg)
        {
            bool isDrive = false;

            if (vehicleType == nameof(Car))
            {
                isDrive = car.Drive(arg);
            }
            else if (vehicleType == nameof(Truck))
            {
                isDrive = truck.Drive(arg);
            }
            else if (vehicleType == nameof(Bus))
            {
                bus.StartAirConditioner();
                isDrive = bus.Drive(arg);
            }

            if (isDrive)
            {
                this.writer.CustomWriteLine($"{vehicleType} travelled {arg} km");
            }
            else
            {
                this.writer.CustomWriteLine($"{vehicleType} needs refueling");
            }
        }

        private void DriveEmptyCommand(IVehicle bus, double arg)
        {
            bus.StopAirConditioner();
            bool isDrive = bus.Drive(arg);

            if (isDrive)
            {
                this.writer.CustomWriteLine($"Bus travelled {arg} km");
            }
            else
            {
                this.writer.CustomWriteLine($"Bus needs refueling");
            }
        }
    }
}
