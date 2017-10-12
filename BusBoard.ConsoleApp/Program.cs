using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BusBoard.Api;

namespace BusBoard.ConsoleApp
{
  class Program
  {

        static void Main(string[] args)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Get stop code from user
            Console.WriteLine("Post code:");
            string postcode = Console.ReadLine();

            // Get number of stops from user
            Console.WriteLine("Number of stops:");
            string stopNumber = Console.ReadLine();

            // Get bus arrival info for 2 closest stops to postcode
            BusStop bStop = new BusStop();
            List<BusStopInfo> arrivals = bStop.GetBusStopArrivalsByPostcode(postcode, Convert.ToInt32(stopNumber));

            // for each stop
            foreach (BusStopInfo a in arrivals)
            {
                // write out stop info
                Console.WriteLine("Buses arriving at stop " + a.Letter + " - " + a.Name + " (" + a.Distance.ToString() + "m away)");

                // write out bus info for this stop
                foreach (BusInfo bus in a.Buses)
                {
                    Console.WriteLine(bus.RouteNumber + " - " + bus.Destination + " - " + bus.TimeToArrival);
                }

                Console.WriteLine();
            }
        }
    }
}
