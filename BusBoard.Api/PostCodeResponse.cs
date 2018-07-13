using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBoard.Api
{
    class PostCodeResponse
    {
        public int status { get; set; }
        public Coordinates result { get; set; }
        public string error { get; set; }
    }
}
