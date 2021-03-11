using System;

namespace ClassBoxData
{
   public  class StartUp
    {
        static void Main(string[] args)
        {
            double lenght = double.Parse(Console.ReadLine());
            double width = double.Parse(Console.ReadLine());
            double height = double.Parse(Console.ReadLine());

            try
            {
                var box = new Box(lenght, width, height);
                Console.WriteLine($"Surface Area - {box.SurfaceArea(lenght, width, height):F2}");
                Console.WriteLine($"Lateral Surface Area - {box.LateralSurfaceArea(lenght, width, height):F2}");
                Console.WriteLine($"Volume - {box.Volume(lenght,width,height):F2}");
            }

            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
