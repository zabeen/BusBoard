using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BusBoard.ConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

      var stopId = PromptForStopId();

      var predictions = new TflApi().GetArrivalPredictions(stopId);
      var predictionsToDisplay = predictions.OrderBy(p => p.TimeToStation).Take(5);
      DisplayPredictions(predictionsToDisplay);

      Console.ReadLine();
    }

    private static string PromptForStopId()
    {
      Console.Write("Enter your stop ID: ");
      return Console.ReadLine(); // Example: "490008660N"
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
