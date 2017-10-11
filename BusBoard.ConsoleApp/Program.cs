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
            string str = Console.ReadLine();
            
            
            // Get Arrival Info using TFL Bus API
            BusAPI bus = new BusAPI();
            //List<string> arrivals = bus.GetArrivalInfoAsString(str, 5);
            // Display info to user
            //arrivals.ForEach(a => Console.WriteLine(a));


            PostcodeAPI pCode = new PostcodeAPI();
            Postcode pc = pCode.RequestPostCodeInfo(str);
            Console.WriteLine(pc.Latitude + " " + pc.Longitude);
            //Console.WriteLine(string.Join(",", bus.RequestStopInfo(pc.Latitude, pc.Longitude)
            //                .Select(r => r.commonName).ToList()));


            List<StopPoint> Info = bus.RequestStopInfo(pc.Latitude, pc.Longitude);
            List<string> s = Info.Select(i => i.commonName).ToList();
            Console.WriteLine(string.Join(",", s));

            Console.ReadLine();
        }
  }
}
