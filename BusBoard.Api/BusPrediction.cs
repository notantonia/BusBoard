using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBoard.Api
{
    public class BusPrediction
    {
        public int timeToStation { get; set; }
        public string lineName { get; set; }
        public string destinationName { get; set; }
        public string stationName { get; set; }

        public string GetNiceTime()
        {
            int timeInMins = (int) Math.Round((double) timeToStation / 60);
            if (timeInMins == 0) return "now";
            else return "in " + timeInMins + (timeInMins == 1 ? " minute" : " minutes");
        }

        public string GetHue()
        {
            return 10 + timeToStation/60 * 11 + "deg";
        }
    }
}