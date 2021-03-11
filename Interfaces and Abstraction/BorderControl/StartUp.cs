using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BorderControl
{
   public class StartUp
    {
        static void Main(string[] args)
        {
            List<IIdentifiable> enteredTheCity = new List<IIdentifiable>();

            string input;

            while ((input = Console.ReadLine()) != "End")
            {
                var inputArgs = input.Split();

                if (inputArgs.Length == 3)
                {
                    enteredTheCity.Add(new Citizen(inputArgs[0], int.Parse(inputArgs[1]), inputArgs[2]));
                }
                else if (inputArgs.Length == 2)
                {
                    enteredTheCity.Add(new Robot(inputArgs[0], inputArgs[1]));
                }
            }

            var lastDigitsOfFakeIds = Console.ReadLine();

            enteredTheCity.Where(c => c.Id.EndsWith(lastDigitsOfFakeIds))
                .Select(c => c.Id)
                .ToList()
                .ForEach(Console.WriteLine);
        }
    }
}
