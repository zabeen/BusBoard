using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BusBoard.ConsoleApp
{
  class Program
  {
    private readonly TflApi tflApi = new TflApi();
    private readonly PostcodesApi postcodesApi = new PostcodesApi();

    static void Main()
    {
      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

      new Program().Run();
    }

    public void Run()
    {
      while (true)
      {
        var postcode = PromptForPostcode();
        var coordinate = postcodesApi.GetCoordinateForPostcode(postcode);

        if (coordinate == null)
        {
          Console.WriteLine("Sorry, I didn't recognise that postcode");
          Console.WriteLine();
          continue;
        }

        var nearbyStops = tflApi.GetStopsNear(coordinate);

        if (nearbyStops.Count == 0)
        {
          Console.WriteLine("Sorry, there are no bus stops near there");
          Console.WriteLine();
          continue;
        }

        foreach (var stop in nearbyStops.Take(2))
        {
          DisplayDepartureBoardForStop(stop);
        }
      }
    }

    private string PromptForPostcode()
    {
      Console.Write("Enter your postcode: ");
      return Console.ReadLine(); // Example: "NW5 1TL"
    }

    private void DisplayDepartureBoardForStop(StopPoint stop)
    {
      Console.WriteLine($"Departure board for {stop.CommonName}");

      var predictions = tflApi.GetArrivalPredictions(stop.NaptanId);

      if (predictions.Count == 0)
      {
        Console.WriteLine("None");
        Console.WriteLine();
        return;
      }

      var predictionsToDisplay = predictions.OrderBy(p => p.TimeToStation).Take(5);
      DisplayPredictions(predictionsToDisplay);

      Console.WriteLine();
    }

    private void DisplayPredictions(IEnumerable<ArrivalPrediction> predictionsToDisplay)
    {
      foreach (var prediction in predictionsToDisplay)
      {
        Console.WriteLine($"{prediction.TimeToStation/60} minutes: {prediction.LineName} to {prediction.DestinationName}");
      }
    }
  }
}
