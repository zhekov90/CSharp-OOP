namespace Presents.Tests
{
    using NUnit.Framework;
    using System;
    [TestFixture]
    public class PresentsTests
    {

        private string DefaultPresentName = "Truck";
        private int DefaultPresentMagic = 50;

        private Present defaultPresent;
        private Bag bag;

        [SetUp]
        public void SetUp()
        {
            this.defaultPresent = new Present(DefaultPresentName, DefaultPresentMagic);
            this.bag = new Bag();
        }

        //Present Tests
        [Test]
        public void PresentConstructorShouldSetCorrectValues()
        {
            Assert.AreEqual(DefaultPresentName, this.defaultPresent.Name);
            Assert.AreEqual(DefaultPresentMagic, this.defaultPresent.Magic);
        }

        //Bag Tests
        [Test]
        public void CreateShouldThrowExceptionWhenPresentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.bag.Create(null);
            }, "Present is null");
        }

        [Test]
        public void CreateShouldThrowExceptionWithExistingPresent()
        {
            this.bag.Create(this.defaultPresent);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.bag.Create(this.defaultPresent);
            }, "This present already exists!");
        }

        [Test]
        public void CreateShouldReturnCorrectMessageWhenSuccessful()
        {
            var expectedResult = $"Successfully added present {DefaultPresentName}.";
            var actualResult = this.bag.Create(this.defaultPresent);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void RemoveShouldReturnTrueWhenSuccessful()
        {
            this.bag.Create(this.defaultPresent);

            var result = this.bag.Remove(this.defaultPresent);

            Assert.That(result, Is.True);
        }

        [Test]
        public void RemoveShouldReturnFalseWhenNotSuccessful()
        {
            var result = this.bag.Remove(this.defaultPresent);

            Assert.That(result, Is.False);
        }

        [Test]
        public void GetPresentWithLeastMagicShouldReturnCorrectPresent()
        {
            this.AddManyPresents();
            var present = this.bag.GetPresentWithLeastMagic();

            Assert.That(present.Name, Is.EqualTo("Hat"));
            Assert.That(present.Magic, Is.EqualTo(20));
        }

        [Test]
        public void GetPresentShouldReturnNullWhenNoMatch()
        {
            var present = this.bag.GetPresent(DefaultPresentName);

            Assert.That(present, Is.Null);
        }

        [Test]
        public void GetPresentShouldReturnCorrectPresentWhenSuccessful()
        {
            this.AddManyPresents();

            var present = this.bag.GetPresent(DefaultPresentName);

            Assert.AreEqual(this.defaultPresent, present);
        }

        [Test]
        public void GetPresentsShouldReturnCorrectCollection()
        {
            this.AddManyPresents();
            var presents = this.bag.GetPresents();

            Assert.That(presents.Count, Is.EqualTo(3));
            Assert.That(this.bag.GetPresents() != null);
        }

        [Test]
        public void GetPresentsShouldReturEmptyCollectionWhenNoPresents()
        {
            var presents = this.bag.GetPresents();

            Assert.That(presents, Is.Empty);
        }
        private void AddManyPresents()
        {
            this.bag.Create(this.defaultPresent);
            this.bag.Create(new Present("Doll", 40));
            this.bag.Create(new Present("Hat", 20));
        }



    }
}
