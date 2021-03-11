using EasterRaces.Core.Contracts;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Cars.Entities;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Drivers.Entities;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Models.Races.Entities;
using EasterRaces.Repositories.Contracts;
using EasterRaces.Repositories.Entities;
using EasterRaces.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasterRaces.Core.Entities
{
    public class ChampionshipController : IChampionshipController
    {
        private IRepository<ICar> carRepository; 
        private IRepository<IDriver> driverRepository; 
        private IRepository<IRace> raceRepository;
        public ChampionshipController()
        {
            this.carRepository = new CarRepository();
            this.driverRepository = new DriverRepository();
            this.raceRepository = new RaceRepository();
        }
        public string AddCarToDriver(string driverName, string carModel)
        {
            IDriver driver = driverRepository.GetByName(driverName);
            if (driver==null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            }
            ICar car = carRepository.GetByName(carModel);
            if (car == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarNotFound, carModel));
            }
            driver.AddCar(car);

            return string.Format(OutputMessages.CarAdded, driverName, carModel);
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            IRace race = raceRepository.GetByName(raceName);
            if (race==null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }
            IDriver driver = driverRepository.GetByName(driverName);
            if (driver == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            }
            race.AddDriver(driver);

            return string.Format(OutputMessages.DriverAdded, driverName, raceName);

        }

        public string CreateCar(string type, string model, int horsePower)
        {
            ICar car = null;
            if (type== "Muscle")
            {
                car = new MuscleCar(model,horsePower);
            }
            else if (type == "Sports")
            {
                car = new SportsCar(model, horsePower);
            }
            ICar checkCar = carRepository.GetByName(model);
            if (checkCar!=null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CarExists, model));
            }
            carRepository.Add(car);

            return string.Format(OutputMessages.CarCreated, car.GetType().Name, car.Model);
            
        }

        public string CreateDriver(string driverName)
        {
            IDriver createDriver = new Driver(driverName);
            IDriver driver = driverRepository.GetByName(driverName);
            if (driver!=null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.DriversExists, driverName));
            }
            driverRepository.Add(createDriver);

            return string.Format(OutputMessages.DriverCreated, driverName);
        }

        public string CreateRace(string name, int laps)
        {
            IRace race = raceRepository.GetByName(name);
            if (race!=null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExists, name));
            }
            IRace curRace = new Race(name,laps);
            raceRepository.Add(curRace);

            return string.Format(OutputMessages.RaceCreated, name);
        }

        public string StartRace(string raceName)
        {
            IRace race = raceRepository.GetByName(raceName);
            if (race==null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }
            List<IDriver> drivers = driverRepository.GetAll().ToList();
            if (drivers.Count<3)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceInvalid, raceName, 3));
            }
            int laps = race.Laps;
            var sortedDrivers = drivers.OrderByDescending(x => x.Car.CalculateRacePoints(laps)).Take(3).ToList();
            raceRepository.Remove(race);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Driver {sortedDrivers[0].Name} wins {raceName} race.");
            sb.AppendLine($"Driver {sortedDrivers[1].Name} is second in {raceName} race.");
            sb.AppendLine($"Driver {sortedDrivers[2].Name} is third in {raceName} race.");

            return sb.ToString().TrimEnd();
        }
    }
}
