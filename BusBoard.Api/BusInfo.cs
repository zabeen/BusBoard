using System;
namespace BusBoard.Api
{
    public class BusInfo
    {
        // ArrivalInfo.LineName
        public string RouteNumber { get; set; }

        // ArrivalInfo.DestinationName
        public string Destination { get; set; }

        // ArrivalInfo.TimetoStationinMinutes
        public string TimeToArrival { get; set; }
    }
}
