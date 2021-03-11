using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Utilities.Messages;
using System;

namespace EasterRaces.Models.Drivers.Entities
{
    public class Driver : IDriver
    {
        private string name;

        public Driver(string name)
        {
            this.Name = name;
        }
        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrEmpty(value)||value.Length<5)//TODO IsNullorWhiteSpace
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidName, value, 5));
                }
                this.name = value;
            }
        }
        public ICar Car { get; private set; }
        public int NumberOfWins { get; private set; }
        public bool CanParticipate => CheckDriverCar();

        public void AddCar(ICar car)
        {
            if (car==null)
            {
                throw new ArgumentNullException(ExceptionMessages.CarInvalid);
            }
            this.Car = car;
        }

        public void WinRace()
        {
            this.NumberOfWins++;
        }
        private bool CheckDriverCar()
        {
            if (this.Car == null)
            {
                return false;
            }
            return true;
        }
    }
}
