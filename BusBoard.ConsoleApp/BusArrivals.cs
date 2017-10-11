using System;
using System.Collections.Generic;
using System.Linq;

namespace BusBoard.ConsoleApp
{
    public class BusArrivals
    {
        public BusArrivals()
        {
        }

        public List<BusArrivalInfo> GetBusArrivalsByPostcode (string postcode, int maxNumberOfStops)
        {
            //Call PostCodeAPI.RequestPostCodeInfo(postcode)
            PostcodeAPI pcAPI = new PostcodeAPI();

            //Returns postcode object with Long and Lat
            Postcode pc = pcAPI.RequestPostCodeInfo(postcode);

            //Call BusAPI.RequestStopInfo(lat, long)
            TFLBusStopAPI stopAPI = new TFLBusStopAPI();

            // Returns List of StopPoints(Bus stops)
            List<StopPoint> stopsList = stopAPI.RequestStopInfo(pc.Latitude, pc.Longitude);

            // Create new list of BusArrivals
            List<BusArrivalInfo> arrivals = new List<BusArrivalInfo>();

            // For each StopPoint (Limit StopPoints to max number of stops, closest first)
            foreach (StopPoint stop in stopsList.OrderBy(s => s.distance).Take(maxNumberOfStops).ToList())
            {
                // Call BusAPI.RequestArrivalInfo with StopPoint.Id
                //Returns list of ArrivalInfo(next buses)
                List<ArrivalInfo> arrInfoList = stopAPI.RequestArrivalInfo(stop.naptanId);

                // for each arrival info - first order list by timetoarrival
                foreach (ArrivalInfo arrInfo in arrInfoList.OrderBy(a => a.TimeToStation).ToList())
                {
                    // Create new BusArrivalInfo
                    BusArrivalInfo busArrival = new BusArrivalInfo()
                    {
                        StopName = stop.commonName,
                        DistanceToStop = stop.distance,
                        StopLetter = stop.stopLetter,
                        RouteNumber = arrInfo.LineName,
                        Destination = arrInfo.DestinationName,
                        TimeToArrival = arrInfo.TimeToStationInMins
                    };

                    // Add to list
                    arrivals.Add(busArrival);
                }
            }

            // return list of bus arrival info
            return arrivals;
        }
    }
}
