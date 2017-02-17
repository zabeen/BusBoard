using RestSharp;

namespace BusBoard.ConsoleApp
{
  public class PostcodesApi
  {
    public Coordinate GetCoordinateForPostcode(string postCode)
    {
      var client = new RestClient(@"http://api.postcodes.io/");
      var request = new RestRequest("postcodes/{postCode}", Method.GET);
      request.AddUrlSegment("postCode", postCode);
      var coordinate = client.Execute<PostcodeResult>(request).Data.Result;
      return coordinate;
    }

    private class PostcodeResult
    {
      // ReSharper disable once UnusedAutoPropertyAccessor.Local - set by RestSharp deserializer
      public Coordinate Result { get; set; }
    }
  }
}