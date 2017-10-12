using System;
namespace BusBoard.ConsoleApp
{
    public class ArrivalInfo
    {
        public string LineName { get; set;}

        public int TimeToStation { get; set; }
        
        public string DestinationName { get; set; }

        public string TimeToStationInMins
        {
            get
            {
                var timespan = TimeSpan.FromSeconds(TimeToStation);
                return timespan.ToString(@"mm\m\:ss\s");
            }
        }
    }
}
