using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
            BusArrivals busArr = new BusArrivals();
            List<BusArrivalInfo> busInfo = busArr.GetBusArrivalsByPostcode(postcode, Convert.ToInt32(stopNumber));

            Console.WriteLine(string.Join("\n", busInfo.Select(b => b.StopLetter + " - " + b.StopName + " - " + b.RouteNumber + " - " + b.TimeToArrival).ToList()));
        }
  }
}
