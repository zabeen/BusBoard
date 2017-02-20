using System.Collections.Generic;
using BusBoard.Web.Models;

namespace BusBoard.Web.ViewModels
{
  public class BusInfo
  {
    public BusInfo(string postCode, IEnumerable<StopWithArrivals> stops)
    {
      PostCode = postCode;
      Stops = stops;
    }

    public string PostCode { get; private set; }
    public IEnumerable<StopWithArrivals> Stops { get; private set; }
  }
}