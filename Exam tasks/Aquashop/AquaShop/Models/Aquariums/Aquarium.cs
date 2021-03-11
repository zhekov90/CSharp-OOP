using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Utilities.Messages;

namespace AquaShop.Models.Aquariums
{
    public abstract class Aquarium : IAquarium
    {
        private string name;
        private readonly List<IDecoration> decorations;
        private readonly List<IFish> fishes;
        protected Aquarium(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.decorations = new List<IDecoration>();
            this.fishes = new List<IFish>();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    var msg = ExceptionMessages.InvalidAquariumName;
                    throw new ArgumentException(msg);
                }

                this.name = value;
            }
        }
        public int Capacity { get; }
        public int Comfort => this.CalculateTotalComfort();

        public ICollection<IDecoration> Decorations =>
            this.decorations.AsReadOnly();

        public ICollection<IFish> Fish =>
            this.fishes.AsReadOnly();

        public void AddFish(IFish fish)
        {
            if (this.fishes.Count == this.Capacity)
            {
                var msg = ExceptionMessages.NotEnoughCapacity;
                throw new InvalidOperationException(msg);
            }

            this.fishes.Add(fish);
        }

        public bool RemoveFish(IFish fish) => this.fishes.Remove(fish);

        public void AddDecoration(IDecoration decoration)
        {
            this.decorations.Add(decoration);
        }

        public void Feed()
        {
            foreach (var fish in this.fishes)
            {
                fish.Eat();
            }
        }

        public string GetInfo()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{this.Name} ({this.GetType().Name}):");

            var fishInfo =
                this.fishes.Any() ?
                    string.Join(", ", fishes.Select(f => f.Name))
                    : "none";

            sb.AppendLine($"Fish: {fishInfo}")
                .AppendLine($"Decorations: {this.Decorations.Count}")
                .AppendLine($"Comfort: {this.Comfort}");

            return sb.ToString().TrimEnd();

        }

        private int CalculateTotalComfort()
        {
            var sum = this.decorations.Sum(d => d.Comfort);
            return sum;
        }
    }
}