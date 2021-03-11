using System;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Utilities.Messages;

namespace AquaShop.Models.Fish
{
    public abstract class Fish : IFish
    {
        private const int MinPrice = 0;

        private string name;
        private string species;
        private decimal price;

        protected Fish(string name, string species, decimal price)
        {
            this.Name = name;
            this.Species = species;
            this.Price = price;
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    var msg = ExceptionMessages.InvalidFishName;
                    ThrowExceptionForInvalidData(msg);
                }

                this.name = value;
            }
        }
        public string Species
        {
            get => this.species;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    var msg = ExceptionMessages.InvalidFishSpecies;
                    ThrowExceptionForInvalidData(msg);
                }

                this.species = value;
            }
        }
        public int Size { get; protected set; }

        public decimal Price
        {
            get => this.price;
            private set
            {
                if (value <= MinPrice)
                {
                    var msg = ExceptionMessages.InvalidFishPrice;
                    ThrowExceptionForInvalidData(msg);
                }

                this.price = value;
            }
        }

        public abstract void Eat();
        private static void ThrowExceptionForInvalidData(string message)
        {
            throw new ArgumentException(message);
        }
    }
}