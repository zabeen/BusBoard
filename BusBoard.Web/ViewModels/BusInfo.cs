using System.Collections.Generic;
using BusBoard.Api;

namespace BusBoard.Web.ViewModels
{
    public class BusInfo
    {
        public string PostCode { get; set; }

        public string NumberOfStops { get; set; }

        public List<BusStopInfo> Stops { get; set; }

        public BusInfo()
        {
            Stops = new List<BusStopInfo>();
        }
    }
}