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

            List<BusPrediction> busPredictions = GetBusPredictions("490008660N");
            foreach (var prediction in busPredictions)
            {
                Console.WriteLine(
                    "Bus: " + prediction.lineName +
                    "\nTo: " + prediction.destinationName +
                    "\nIn: " + Math.Round((double)prediction.timeToStation / 60) + " minute(s)\n");
            }

            Coordinates coordinates = GetCoordinates("NW5 1TL");
            Console.WriteLine(coordinates.longitude + ", " + coordinates.latitude);

            List<StopPoint> stopPoints = GetTwoClosestStopPoints(coordinates);

            foreach (var stopPoint in stopPoints)
            {
                Console.WriteLine(stopPoint.naptanId);
            }

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

        static List<BusPrediction> GetBusPredictions(string stopPoint)
        {
            // TODO: Add API keys
            var TFLClient = new RestClient("https://api.tfl.gov.uk");

            var request = new RestRequest("StopPoint/{id}/Arrivals", Method.GET);
            request.AddUrlSegment("id", stopPoint);

            IRestResponse<List<BusPrediction>> response = TFLClient.Execute<List<BusPrediction>>(request);

            return response.Data;
        }

        static List<StopPoint> GetTwoClosestStopPoints(Coordinates coords)
        {
            // TODO: Add API keys
            var TFLClient = new RestClient("https://api.tfl.gov.uk");

            var request = new RestRequest("StopPoint/", Method.GET);
            request.AddParameter("stopTypes", "NaptanPublicBusCoachTram");
            request.AddParameter("lat", coords.latitude);
            request.AddParameter("lon", coords.longitude);

            IRestResponse<StopPointResponse> response = TFLClient.Execute<StopPointResponse>(request);

            List<StopPoint> stopPoints = response.Data.stopPoints;
            List<StopPoint> orderedStopPoints = stopPoints.OrderBy(stopPoint => stopPoint.distance).ToList();
            List<StopPoint> twoClosest = orderedStopPoints.GetRange(0, 2);

            return twoClosest;
        }
    }
}
