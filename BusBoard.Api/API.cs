using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace BusBoard.Api
{
    public class API
    {
        public const int STATUS_OKAY = 200;

        public static List<BusPrediction> GetBusPredictions(string postCode, int stops, int predictions)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Coordinates coordinates = GetCoordinates(postCode);
            List<StopPoint> stopPoints = GetClosestStopPoints(coordinates, stops);

            List<BusPrediction> busPredictions = new List<BusPrediction>();
            foreach (var stopPoint in stopPoints)
            {
                busPredictions.AddRange(GetBusPredictions(stopPoint.naptanId));
            }

            busPredictions = busPredictions.OrderBy(busPrediction => busPrediction.timeToStation).ToList();

            return busPredictions.GetRange(0, predictions);
        }

        static Coordinates GetCoordinates(string postCodeName)
        {
            var postCodeClient = new RestClient("https://api.postcodes.io");

            var request = new RestRequest("postcodes/{name}", Method.GET);
            request.AddUrlSegment("name", postCodeName);

            IRestResponse<PostCodeResponse> response = postCodeClient.Execute<PostCodeResponse>(request);

            if (response.Data.status == STATUS_OKAY)
            {
                return response.Data.result;
            }
            else
            {
                throw new Exception(response.Data.error);
            }
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

        static List<StopPoint> GetClosestStopPoints(Coordinates coords, int stops)
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
            List<StopPoint> twoClosest = orderedStopPoints.GetRange(0, stops);

            return twoClosest;
        }
    }
}
