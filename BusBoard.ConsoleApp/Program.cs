using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace BusBoard.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // TODO: Add API keys
            var TFLClient = new RestClient("https://api.tfl.gov.uk");

            var request = new RestRequest("StopPoint/{id}/Arrivals", Method.GET);
            request.AddUrlSegment("id", "490008660N");

            IRestResponse<List<BusPrediction>> response = TFLClient.Execute<List<BusPrediction>>(request);
            foreach (var prediction in response.Data)
            {
                Console.WriteLine(
                    "Bus: " + prediction.lineName +
                    "\nTo: " + prediction.destinationName +
                    "\nIn: " + Math.Round((double)prediction.timeToStation / 60) + " minute(s)\n");
            }

            Coordinates coordinates = GetCoordinates("NW5 1TL");
            Console.WriteLine(coordinates.longitude + ", " + coordinates.latitude);

            Console.Read();
        }

        static Coordinates GetCoordinates(string postCodeName)
        {
            var postCodeClient = new RestClient("https://api.postcodes.io");

            var request = new RestRequest("postcodes/{name}", Method.GET);
            request.AddUrlSegment("name", postCodeName);

            IRestResponse<PostCodeResponse> response = postCodeClient.Execute<PostCodeResponse>(request);

            return response.Data.result;
        }
    }
}
