using RestSharp;

namespace BusBoard.Api
{
  public class PostcodesApi
  {
    private readonly RestClient restClient = new RestClient(@"http://api.postcodes.io/");

    public Coordinate GetCoordinateForPostcode(string postCode)
    {
      var request = new RestRequest("postcodes/{postCode}", Method.GET);
      request.AddUrlSegment("postCode", postCode);
      var coordinate = restClient.Execute<PostcodeResult>(request).Data.Result;
      return coordinate;
    }

    private class PostcodeResult
    {
      // ReSharper disable once UnusedAutoPropertyAccessor.Local - set by RestSharp deserializer
      public Coordinate Result { get; set; }
    }
  }
}