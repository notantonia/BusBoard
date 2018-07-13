using System.Collections.Generic;
using BusBoard.Api;

namespace BusBoard.Web.ViewModels
{
    public class BusInfo
    {
        public string PostCode { get; set; }
        public List<BusPrediction> BusPredictions { get; set; }
        public List<StopPoint> StopPoints { get; set; }
        public string ErrorMessage { get; set; }

        public BusInfo(string postCode, List<BusPrediction> busPredictions, List<StopPoint> stopPoints)
        {
            PostCode = postCode;
            BusPredictions = busPredictions;
            StopPoints = stopPoints;
        }

        public BusInfo(string postCode, string errorMessage)
        {
            PostCode = postCode;
            ErrorMessage = errorMessage;
        }
    }
}