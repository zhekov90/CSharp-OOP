using NUnit.Framework;

namespace Tests
{
//    using CarManager;
    using System;

    public class CarTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConstructorWorksCorrectly()
        {
            string make = "VW";
            string model = "Golf";
            double fuelConsumption = 2;
            double fuelCap = 100;

            Car car = new Car(make, model, fuelConsumption, fuelCap);

            Assert.AreEqual(make, car.Make);
            Assert.AreEqual(model, car.Model);
            Assert.AreEqual(fuelConsumption, car.FuelConsumption);
            Assert.AreEqual(fuelCap, car.FuelCapacity);
        }
        
        [Test]
        [TestCase(null, "asd", 2, 5)]
        [TestCase("asd", null, 5, 2)]
        [TestCase("asd", "dsa", 0, 10)]
        [TestCase("asd", "dsa", -1, 10)]
        [TestCase("asd", "dsa", 10, 0)]
        [TestCase("asd", "dsa", 10, -1)]
        public void AllProperties_ThrowArgumentException_WhenInvalid
            (string make, string model, double fuelCon, double fuelCap)
        {
            Assert.Throws<ArgumentException>(()
                => new Car(make, model, fuelCon, fuelCap));
        }

        [Test]
        public void RefuelNormally()
        {
            string make = "VW";
            string model = "Golf";
            double fuelConsumption = 2;
            double fuelCap = 100;

            Car car = new Car(make, model, fuelConsumption, fuelCap);
            car.Refuel(10);
            double expectedFuelAmount = 10;
            double actualFuelAmount = car.FuelAmount;

            Assert.AreEqual(expectedFuelAmount, actualFuelAmount);
        }

        [Test]
        public void RefuelCapsCorrectly()
        {
            string make = "VW";
            string model = "Golf";
            double fuelConsumption = 2;
            double fuelCap = 100;

            Car car = new Car(make, model, fuelConsumption, fuelCap);
            double tryToFuelAmount = 1000;
            car.Refuel(tryToFuelAmount);

            double expectedFuelAmount = fuelCap;
            double actualFuelAmount = car.FuelAmount;

            Assert.AreEqual(expectedFuelAmount, actualFuelAmount);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-10)]
        public void Refuel_ArgumentException_Negative(double inputAmount)
        {
            string make = "VW";
            string model = "Golf";
            double fuelConsumption = 2;
            double fuelCap = 100;
            Car car = new Car(make, model, fuelConsumption, fuelCap);

            Assert.Throws<ArgumentException>(
                () => car.Refuel(inputAmount));
        }

        [Test]
        public void DriveNormally()
        {
            string make = "VW";
            string model = "Golf";
            double fuelConsumption = 2;
            double fuelCap = 100;
            Car car = new Car(make, model, fuelConsumption, fuelCap);

            car.Refuel(20);
            car.Drive(20);

            double expectedFuelAmount = 19.6;
            double actualFuelAmount = car.FuelAmount;

            Assert.AreEqual(expectedFuelAmount, actualFuelAmount);
        }

        [Test]
        [TestCase(1, 100)]
        [TestCase(1, 51)]
        [TestCase(9, 500)]
        [TestCase(8, 444)]
        [TestCase(10, 501)]
        [TestCase(11, 666)]
        public void Drive_ThrowInvalidOperationException_NotEnoughFuel
            (double refuelAmount, double driveDistance)
        {
            string make = "VW";
            string model = "Golf";
            double fuelConsumption = 2;
            double fuelCap = 100;
            Car car = new Car(make, model, fuelConsumption, fuelCap);

            car.Refuel(refuelAmount);

            Assert.Throws<InvalidOperationException>(
                () => car.Drive(driveDistance));
        }
    }
}