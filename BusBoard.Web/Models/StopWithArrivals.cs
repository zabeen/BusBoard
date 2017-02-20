using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusBoard.Api;

namespace BusBoard.Web.Models
{
  public class StopWithArrivals
  {
    public StopWithArrivals(string name, IEnumerable<ArrivalPrediction> arrivals)
    {
      Name = name;
      Arrivals = arrivals;
    }

    public string Name { get; private set; }
    public IEnumerable<ArrivalPrediction> Arrivals { get; private set; }
  }
}