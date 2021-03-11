using NUnit.Framework;

namespace Aquariums.Tests
{
    using System;

    public class AquariumsTests
    {
        private const string DefaultFishName = "Nemo";
        private const string DefaultAquariumName = "Underworld";
        private const int DefaultAquariumCapacity = 5;

        private Fish defaultFish;
        private Aquarium defaultAquarium;

        [SetUp]
        public void SetUp()
        {
            this.defaultFish = new Fish(DefaultFishName);
            this.defaultAquarium = new Aquarium
                (DefaultAquariumName, DefaultAquariumCapacity);
        }

        //Fish Tests
        [Test]
        public void FishConstructorShouldSetCorrectValues()
        {
            Assert.AreEqual(DefaultFishName, this.defaultFish.Name);
            Assert.That(this.defaultFish.Available, Is.True);
        }

        //Aquarium Tests
        [Test]
        public void AquariumConstructorShouldSetCorrectValues()
        {
            Assert.AreEqual(DefaultAquariumName, this.defaultAquarium.Name);
            Assert.AreEqual(DefaultAquariumCapacity, this.defaultAquarium.Capacity);
        }

        [Test]
        public void NameShouldThrowExceptionWhenNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    this.defaultAquarium = new Aquarium(null, DefaultAquariumCapacity);
                }, "Invalid aquarium name!");
        }

        [Test]
        public void NameShouldThrowExceptionWhenEmpty()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    this.defaultAquarium = new Aquarium(string.Empty, DefaultAquariumCapacity);
                }, "Invalid aquarium name!");
        }

        [Test]
        public void CapacityShouldThrowExceptionWhenNegative()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                this.defaultAquarium = new Aquarium(DefaultAquariumName, -20);
            }, "Invalid aquarium capacity!");
        }

        [Test]
        public void AddFishShouldAddFishSuccessfully()
        {
            this.AddManyFishes();

            Assert.That(this.defaultAquarium.Count, Is.EqualTo(4));
        }

        [Test]
        public void AddShouldThrowExceptionWhenNoMoreSpace()
        {
            this.AddManyFishes();
            this.defaultAquarium.Add(new Fish("Greeny"));

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.defaultAquarium.Add(new Fish("Blue"));
            }, "Aquarium is full!");
        }

        [Test]
        public void RemoveShouldWorkProperlyWithValidFish()
        {
            this.AddManyFishes();

            this.defaultAquarium.RemoveFish("Dorry");

            var expectedCount = 3;
            var actualCount = this.defaultAquarium.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void RemoveShouldThrowExceptionWithInvalidFish()
        {
            this.AddManyFishes();

            var missingName = "Blue";

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.defaultAquarium.RemoveFish(missingName);
            }, $"Fish with the name {missingName} doesn't exist!");
        }

        [Test]
        public void SellFishShouldSetUnavailableStatusWhenSuccessful()
        {
            this.AddManyFishes();

            var soldFish = this.defaultAquarium.SellFish(DefaultFishName);

            Assert.That(this.defaultFish.Available, Is.False);
            Assert.AreEqual(this.defaultFish, soldFish);
        }

        [Test]
        public void SellFishShouldThrowExceptionWithInvalidFish()
        {
            this.AddManyFishes();

            var missingName = "Blue";

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.defaultAquarium.SellFish(missingName);
            }, $"Fish with the name {missingName} doesn't exist!");
        }

        [Test]
        public void ReportShouldReturnCorrectString()
        {
            this.AddManyFishes();

            var fishInfo = "Nemo, Dorry, Emerald, Diamond";
            var expected = $"Fish available at {this.defaultAquarium.Name}: {fishInfo}";

            Assert.AreEqual(expected, this.defaultAquarium.Report());
        }

        [Test]
        public void ReportShouldReturnEmptyStringWithNoFishes()
        {
            var fishInfo = string.Empty;
            var expected = $"Fish available at {this.defaultAquarium.Name}: {fishInfo}";

            Assert.AreEqual(expected, this.defaultAquarium.Report());
        }
        private void AddManyFishes()
        {
            this.defaultAquarium.Add(defaultFish);
            this.defaultAquarium.Add(new Fish("Dorry"));
            this.defaultAquarium.Add(new Fish("Emerald"));
            this.defaultAquarium.Add(new Fish("Diamond"));
        }
    }
}