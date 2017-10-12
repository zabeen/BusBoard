using System;
using System.Collections.Generic;
using System.Linq;

namespace BusBoard.ConsoleApp
{
    public class BusStop
    {
        public BusStop()
        {
        }

        public List<BusArrivalInfo> GetBusStopArrivalsByPostcode (string postcode, int maxNumberOfStops)
        {
            //Call PostCodeAPI.RequestPostCodeInfo(postcode)
            PostcodeAPI pcAPI = new PostcodeAPI();

            //Returns postcode object with Long and Lat
            PostcodeInfo pc = pcAPI.RequestPostcodeInfo(postcode);

            //Call BusAPI.RequestStopInfo(lat, long)
            TFLStopPointAPI stopAPI = new TFLStopPointAPI();

            // Returns List of StopPoints(Bus stops)
            List<StopPointInfo> stopsList = stopAPI.RequestStopInfo(pc.Latitude, pc.Longitude);

            // Create new list of BusArrivals
            List<BusArrivalInfo> arrivals = new List<BusArrivalInfo>();

            // For each StopPoint (Limit StopPoints to max number of stops, closest first)
            foreach (StopPointInfo stop in stopsList.OrderBy(s => s.distance).Take(maxNumberOfStops).ToList())
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
