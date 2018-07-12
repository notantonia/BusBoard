using System;
using BusBoard.Api;

namespace BusBoard.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What's your postcode?");
            string input = Console.ReadLine();

            Console.WriteLine();

            var busPredictions = API.GetBusPredictions(input, 2, 5);

            foreach (var prediction in busPredictions)
            {
                Console.WriteLine(
                    "Stop: " + prediction.stationName +
                    "\nBus: " + prediction.lineName +
                    "\nTo: " + prediction.destinationName +
                    "\nIn: " + Math.Round((double)prediction.timeToStation / 60) + " minute(s)\n");
            }

            Console.Read();
        }
    }
}
