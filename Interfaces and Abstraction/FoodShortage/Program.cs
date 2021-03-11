using FoodShortage.Interfaces;
using FoodShortage.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodShortage
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<IBuyer> buyers = new List<IBuyer>();
           
            int numberOfPeople = int.Parse(Console.ReadLine());
            for (int i = 0; i < numberOfPeople; i++)
            {

                var input = Console.ReadLine().Split();
                if (input.Length == 4)
                {
                    string name = input[0];
                    int age = int.Parse(input[1]);
                    string id = input[2];
                    string birthdate = input[3];

                    Citizen citizen = new Citizen(name, age, id, birthdate);
                    buyers.Add(citizen);
                }
                else if (input.Length == 3)
                {
                    string name = input[0];
                    int age = int.Parse(input[1]);
                    string group = input[2];

                    Rebel rebel = new Rebel(name, age, group);
                    buyers.Add(rebel);
                }
            }

                string command = Console.ReadLine();
                while (command!="End")
                {
                    string name = command;

                    var buyer = buyers.SingleOrDefault(b => b.Name == name);
                    if (buyer!=null)
                    {
                        buyer.BuyFood();
                    }

                    command = Console.ReadLine();
                }

            
                Console.WriteLine(buyers.Sum(b=>b.Food));
           
           
        }
    }
}
