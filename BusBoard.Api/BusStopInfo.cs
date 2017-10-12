using System;
using System.Collections.Generic;

namespace BusBoard.Api
{
    public class BusStopInfo
    {
        // StopInfo.CommonName
        public string Name { get; set; }

        // StopInfo.Distance
        public int Distance { get; set; }

        // StopInfo.StopLetter
        public string Letter { get; set; }

        public List<BusInfo> Buses;

        public BusStopInfo()
        {
            Buses = new List<BusInfo>();
        }
    
    }
}
