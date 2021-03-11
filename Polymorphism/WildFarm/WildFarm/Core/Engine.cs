﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WildFarm.Animals;
using WildFarm.Animals.Contracts;
using WildFarm.Exceptions;
using WildFarm.Factories;
using WildFarm.Foods;
using WildFarm.Foods.Contracts;

namespace WildFarm.Core
{
    public class Engine : IEngine
    {
        private ICollection<IAnimal> animals;
        private FoodFactory foodFactory;
        public Engine()
        {
            this.animals = new List<IAnimal>();
            this.foodFactory = new FoodFactory();
        }
        public void Run()
        {
            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                string[] animalArgs = command
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();


                IAnimal animal = ProduceAnimal(animalArgs);
                animals.Add(animal);

                command = Console.ReadLine();
                string[] foodArgs = command
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                IFood food = this.foodFactory.ProduceFood(foodArgs[0], int.Parse(foodArgs[1]));

                Console.WriteLine(animal.ProduceSound());

                try
                {
                    animal.Feed(food);
                    
                }
                catch (UneatableFoodException ufe)
                {
                    Console.WriteLine(ufe.Message);
                }


            }
            if (command=="End")
            {
                foreach (IAnimal animal in this.animals)
                {
                    Console.WriteLine(animal);
                }
            }
               

        }

        private static IAnimal ProduceAnimal(string[] animalArgs)
        {
            IAnimal animal = null;

            string animalType = animalArgs[0];
            string name = animalArgs[1];
            double weight = double.Parse(animalArgs[2]);

            if (animalType == "Owl")
            {
                double wingSize = double.Parse(animalArgs[3]);
                animal = new Owl(name, weight, wingSize);
            }
            else if (animalType == "Hen")
            {
                double wingSize = double.Parse(animalArgs[3]);
                animal = new Hen(name, weight, wingSize);
            }
            else
            {
                string livingRegion = animalArgs[3];
                if (animalType == "Dog")
                {
                    animal = new Dog(name, weight, livingRegion);
                }
                else if (animalType == "Mouse")
                {
                    animal = new Mouse(name, weight, livingRegion);
                }
                else if (animalType == "Cat" || animalType == "Tiger")
                {
                    string breed = animalArgs[4];

                    if (animalType == "Cat")
                    {
                        animal = new Cat(name, weight, livingRegion, breed);
                    }
                    else if (animalType == "Tiger")
                    {
                        animal = new Tiger(name, weight, livingRegion, breed);
                    }

                }
            }

            return animal;
        }
    }
}
