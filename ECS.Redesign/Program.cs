using System;

namespace ECS.Redesign
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing ECS.Redesign");

            // Create real dependencies
            var heater = new Heater();
            var tempSensor = new TempSensor();

            // Make an ECS with a threshold of 23
            var control = new ECS(tempSensor, heater, 23);

            for (int i = 1; i <= 15; i++)
            {
                Console.WriteLine($"Running regulation number {i}");

                control.Regulate();
            }


        }
    }
}
