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
    }
}