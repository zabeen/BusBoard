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
  }
}