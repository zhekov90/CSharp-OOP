using System;
using System.Collections.Generic;
using System.Linq;

namespace Birthday
{
    public class Program
    {
        static void Main(string[] args)
        {

            List<IBirthable> enteredTheCity = new List<IBirthable>();
            List<IBirthable> filtered = new List<IBirthable>();
            List<IIdentifiable> robots = new List<IIdentifiable>();

            string input;

            while ((input = Console.ReadLine()) != "End")
            {
                var inputArgs = input.Split();

                if (inputArgs[0] == "Citizen")
                {
                    enteredTheCity.Add(new Citizen(inputArgs[1], int.Parse(inputArgs[2]), inputArgs[3], inputArgs[4]));
                }
                else if (inputArgs[0] == "Robot")
                {
                    robots.Add(new Robot(inputArgs[1], inputArgs[2]));
                }
                else if (inputArgs[0] == "Pet")
                {
                    enteredTheCity.Add(new Pet(inputArgs[1], inputArgs[2]));
                }
            }

            var year = Console.ReadLine();


            filtered= enteredTheCity.Where(c => c.Birthdate.EndsWith(year)).ToList();

            if (filtered.Any())
            {
                foreach (var inhabitant in filtered)
                {
                    Console.WriteLine(inhabitant.Birthdate);
                }
            }
           


        }
    }
}
