using System;
using System.Collections.Generic;
using System.Linq;

namespace BusBoard.ConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
      var postcode = PromptForPostcode();

      var coordinate = new PostcodesApi().GetCoordinateForPostcode(postcode);
      var nearbyStops = new TflApi().GetStopsNear(coordinate);

      foreach (var stop in nearbyStops.Take(2))
      {
        Console.WriteLine($"Departure board for {stop.CommonName}");

        var predictions = new TflApi().GetArrivalPredictions(stop.NaptanId);
        var predictionsToDisplay = predictions.OrderBy(p => p.TimeToStation).Take(5);
        DisplayPredictions(predictionsToDisplay);

        Console.WriteLine();
      }

      Console.ReadLine();
    }

    private static string PromptForPostcode()
    {
      Console.Write("Enter your postcode: ");
      return Console.ReadLine(); // Example: "NW5 1TL"
    }

    private static void DisplayPredictions(IEnumerable<ArrivalPrediction> predictionsToDisplay)
    {
      foreach (var prediction in predictionsToDisplay)
      {
        Console.WriteLine($"{prediction.TimeToStation/60} minutes: {prediction.LineName} to {prediction.DestinationName}");
      }
    }
  }
}
