using NUnit.Framework;
using System;
using System.Collections.Generic;
using TheRace;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void AddDriverShouldThrowExceptionWhenInvalidDriver()
        {
            var raceEntry = new RaceEntry();
            Assert.Throws<InvalidOperationException>(() =>
            {
                raceEntry.AddDriver(null);
            });
        }

        [Test]
        public void AddDriverShouldThrowExceptionWhenExsistingDriver()
        {
            var raceEntry = new RaceEntry();
            var driver = new UnitDriver("pesho", new UnitCar("lada", 146, 86));
            raceEntry.AddDriver(driver);
            Assert.Throws<InvalidOperationException>(() =>
            {
                raceEntry.AddDriver(driver);
            });
        }

        [Test]
        public void AddDriverCorrectly()
        {
            var raceEntry = new RaceEntry();
            var driver = new UnitDriver("pesho", new UnitCar("lada", 146, 86));
            raceEntry.AddDriver(driver);
            Assert.AreEqual(1, raceEntry.Counter);
        }

        [Test]
        public void AddDriverCorrectName()
        {
            var raceEntry = new RaceEntry();
            var driver = new UnitDriver("pesho", new UnitCar("lada", 146, 86));
            raceEntry.AddDriver(driver);
            Assert.AreEqual("pesho", driver.Name);
        }

        [Test]
        public void CalculateAverageHorsePowerShouldThrowExceptionWhenNotEnoughDrivers()
        {
            var raceEntry = new RaceEntry();
            var driver = new UnitDriver("pesho", new UnitCar("lada", 146, 86));
            raceEntry.AddDriver(driver);
            Assert.Throws<InvalidOperationException>(() =>
            {
                raceEntry.CalculateAverageHorsePower();
            });
        }

        [Test]
        public void CalculateAverageHorsePowerShouldWork()
        {
            var raceEntry = new RaceEntry();
            var driver = new UnitDriver("pesho", new UnitCar("lada", 100, 100));
            var driver2 = new UnitDriver("gosho", new UnitCar("bmw", 180, 100));
            raceEntry.AddDriver(driver);
            raceEntry.AddDriver(driver2);
            Assert.AreEqual(140, raceEntry.CalculateAverageHorsePower());
        }
    }
}