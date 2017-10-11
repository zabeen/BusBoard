using System;
using System.Collections.Generic;
using RestSharp;
using RestSharp.Authenticators;
using System.Linq;

namespace BusBoard.ConsoleApp
{
    public class PostcodeAPI
    {
        public RestClient client = new RestClient();

        public PostcodeAPI()
        {
            // create Rest client with Postcode API URL
            client.BaseUrl = new Uri("http://api.postcodes.io");
        }

        public Postcode RequestPostCodeInfo(string postCode)
        {
            // Create request
            var request = new RestRequest
            {
                Resource = "postcodes/" + postCode,
                RootElement = "result"
            };
            var response = client.Execute<Postcode>(request);

            return response.Data;
        }
    }
}
