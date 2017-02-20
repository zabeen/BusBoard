using System.Collections.Generic;
using RestSharp;

namespace BusBoard.Api
{
  public class TflApi
  {
    private readonly RestClient restClient = new RestClient(@"https://api.tfl.gov.uk");

    public List<ArrivalPrediction> GetArrivalPredictions(string stopId)
    {
      var request = new RestRequest("StopPoint/{stopId}/Arrivals", Method.GET);
      request.AddUrlSegment("stopId", stopId);
      var predictions = restClient.Execute<List<ArrivalPrediction>>(request).Data;
      return predictions;
    }

    public List<StopPoint> GetStopsNear(Coordinate coordinate)
    {
      var request = new RestRequest("StopPoint", Method.GET);
      request.AddParameter("stopTypes", "NaptanPublicBusCoachTram");
      request.AddParameter("lat", coordinate.Latitude);
      request.AddParameter("lon", coordinate.Longitude);
      request.AddParameter("radius", 1000);
      var stops = restClient.Execute<StopPointResult>(request).Data.StopPoints;
      return stops;
    }

    private class StopPointResult
    {
      // ReSharper disable once UnusedAutoPropertyAccessor.Local - set by RestSharp deserializer
      public List<StopPoint> StopPoints { get; set; }
    }
  }
}