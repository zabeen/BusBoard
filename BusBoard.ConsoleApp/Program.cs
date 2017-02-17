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
      Console.WriteLine($"Your postcode is at {coordinate.Latitude}, {coordinate.Longitude}");

      /*
      var predictions = new TflApi().GetArrivalPredictions(stopId);
      var predictionsToDisplay = predictions.OrderBy(p => p.TimeToStation).Take(5);
      DisplayPredictions(predictionsToDisplay);
      */

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
