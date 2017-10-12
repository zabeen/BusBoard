using System;
using System.Collections.Generic;
using System.Linq;

namespace BusBoard.Api
{
    public class BusStop
    {
        public BusStop()
        {
        }

        public List<BusStopInfo> GetBusStopArrivalsByPostcode (string postcode, int maxNumberOfStops)
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
            List<BusStopInfo> bStops = new List<BusStopInfo>();

            // For each StopPoint (Limit StopPoints to max number of stops, closest first)
            foreach (StopPointInfo stop in stopsList.OrderBy(s => s.distance).Take(maxNumberOfStops).ToList())
            {
                // Initialise a new bus stop object
                BusStopInfo bStop = new BusStopInfo()
                {
                    Name = stop.commonName,
                    Distance = stop.distance,
                    Letter = stop.stopLetter
                };

                // Call BusAPI.RequestArrivalInfo with StopPoint.Id
                //Returns list of ArrivalInfo(next buses)
                List<ArrivalInfo> arrInfoList = stopAPI.RequestArrivalInfo(stop.naptanId);

                // for each arrival info - first order list by timetoarrival
                foreach (ArrivalInfo arrInfo in arrInfoList.OrderBy(a => a.TimeToStation).ToList())
                {
                    // Create new BusInfo
                    BusInfo bus = new BusInfo()
                    {                       
                        RouteNumber = arrInfo.LineName,
                        Destination = arrInfo.DestinationName,
                        TimeToArrival = arrInfo.TimeToStationInMins
                    };

                    // Add to bus list
                    bStop.Buses.Add(bus);
                }

                // Add completed bus stop to list
                bStops.Add(bStop);
            }

            // return list of bus arrival info
            return bStops;
        }
    }
}
