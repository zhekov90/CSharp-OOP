using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Repositories;
using AquaShop.Repositories.Contracts;
using AquaShop.Utilities.Messages;

namespace AquaShop.Core
{
    public class Controller : IController
    {
        private readonly IRepository<IDecoration> decorations;
        private readonly ICollection<IAquarium> aquariums;

        public Controller()
        {
            this.aquariums = new List<IAquarium>();
            this.decorations = new DecorationRepository();
        }


        public string AddAquarium(string aquariumType, string aquariumName)
        {
            var currentAquarium = aquariumType switch
            {
                "FreshwaterAquarium" => (IAquarium)new FreshwaterAquarium(aquariumName),
                "SaltwaterAquarium" => new SaltwaterAquarium(aquariumName),
                _ => throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType)
            };

            this.aquariums.Add(currentAquarium);
            return $"Successfully added {currentAquarium.GetType().Name}.";
        }

        public string AddDecoration(string decorationType)
        {
            IDecoration currentDecoration;
            switch (decorationType)
            {
                case "Ornament":
                    currentDecoration = new Ornament();
                    break;
                case "Plant":
                    currentDecoration = new Plant();
                    break;
                default:
                    throw new InvalidOperationException(ExceptionMessages.InvalidDecorationType);
            }

            this.decorations.Add(currentDecoration);
            return $"Successfully added {currentDecoration.GetType().Name}.";
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            if (this.decorations.Models.All(d => d.GetType().Name != decorationType))
            {
                var msg = string.Format(ExceptionMessages.InexistentDecoration, decorationType);
                throw new InvalidOperationException(msg);
            }

            var aquarium = this.aquariums.First(a => a.Name == aquariumName);
            var decoration = this.decorations.FindByType(decorationType);

            aquarium.AddDecoration(decoration);
            this.decorations.Remove(decoration);

            var outputMsg = string.Format(OutputMessages.EntityAddedToAquarium,
                decorationType, aquariumName);

            return outputMsg;
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            IFish currentFish;
            string possibleAquarium;
            switch (fishType)
            {
                case "FreshwaterFish":
                    currentFish = new FreshwaterFish(fishName, fishSpecies, price);
                    possibleAquarium = "FreshwaterAquarium";
                    break;
                case "SaltwaterFish":
                    currentFish = new SaltwaterFish(fishName, fishSpecies, price);
                    possibleAquarium = "SaltwaterAquarium";
                    break;
                default:
                    throw new InvalidOperationException(ExceptionMessages.InvalidFishType);
            }

            var aquarium = this.aquariums.First(a => a.Name == aquariumName);

            string outputMsg;

            if (possibleAquarium != aquarium.GetType().Name)
            {
                outputMsg = "Water not suitable.";
            }
            else
            {
                aquarium.AddFish(currentFish);
                outputMsg = string.Format(OutputMessages.EntityAddedToAquarium,
                    fishType, aquariumName);
            }

            return outputMsg;
        }

        public string FeedFish(string aquariumName)
        {
            var aquarium = this.aquariums.First(a => a.Name == aquariumName);

            aquarium.Feed();

            var count = aquarium.Fish.Count;

            var outputMsg = string.Format(OutputMessages.FishFed, count);
            return outputMsg;
        }

        public string CalculateValue(string aquariumName)
        {
            var aquarium = this.aquariums.First(a => a.Name == aquariumName);

            var sum = aquarium.Decorations.Sum(d => d.Price) +
                      aquarium.Fish.Sum(f => f.Price);

            var outputMsg = string.Format(OutputMessages.AquariumValue,
                aquariumName, sum);

            return outputMsg;
        }

        public string Report()
        {
            var sb = new StringBuilder();
            foreach (var aquarium in this.aquariums)
            {
                sb.AppendLine(aquarium.GetInfo());
            }

            return sb.ToString().TrimEnd();
        }
    }
}