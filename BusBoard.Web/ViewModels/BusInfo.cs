﻿using System.Collections.Generic;
using BusBoard.Api;

namespace BusBoard.Web.ViewModels
{
    public class BusInfo
    {
        public string PostCode { get; set; }
        public List<BusPrediction> BusPredictions { get; set; }

        public BusInfo(string postCode, List<BusPrediction> busPredictions)
        {
            PostCode = postCode;
            BusPredictions = busPredictions;
        }
    }
}