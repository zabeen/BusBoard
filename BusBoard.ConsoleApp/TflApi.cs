using System.Collections.Generic;
using RestSharp;

namespace BusBoard.ConsoleApp
{
  public class TflApi
  {
    public List<ArrivalPrediction> GetArrivalPredictions(string stopId)
    {
      var client = new RestClient(@"https://api.tfl.gov.uk");
      var request = new RestRequest("StopPoint/{stopId}/Arrivals", Method.GET);
      request.AddUrlSegment("stopId", stopId);
      var predictions = client.Execute<List<ArrivalPrediction>>(request).Data;
      return predictions;
    }

    public List<StopPoint> GetStopsNear(Coordinate coordinate)
    {
      var client = new RestClient(@"https://api.tfl.gov.uk");
      var request = new RestRequest("StopPoint", Method.GET);
      request.AddParameter("stopTypes", "NaptanPublicBusCoachTram");
      request.AddParameter("lat", coordinate.Latitude);
      request.AddParameter("lon", coordinate.Longitude);
      var stops = client.Execute<StopPointResult>(request).Data.StopPoints;
      return stops;
    }

    private class StopPointResult
    {
      // ReSharper disable once UnusedAutoPropertyAccessor.Local - set by RestSharp deserializer
      public List<StopPoint> StopPoints { get; set; }
    }
  }
}