using System;
namespace BusBoard.Api
{
    public class BusArrivalInfo
    {
        // StopInfo.CommonName
        public string StopName
        {
            get;
            set;
        }

        // StopInfo.Distance
        public int DistanceToStop
        {
            get;
            set;
        }

        // StopInfo.StopLetter
        public string StopLetter
        {
            get;
            set;
        }
  
        // ArrivalInfo.LineName
        public string RouteNumber
        {
            get;
            set;
        }

        // ArrivalInfo.DestinationName
        public string Destination
        {
            get;
            set;
        }

        // ArrivalInfo.TimetoStationinMinutes
        public string TimeToArrival
        {
            get;
            set;
        }
    
    }
}
