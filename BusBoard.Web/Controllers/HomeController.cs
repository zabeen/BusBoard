using System;
using System.Linq;
using System.Web.Mvc;
using BusBoard.Api;
using BusBoard.Web.Models;
using BusBoard.Web.ViewModels;

namespace BusBoard.Web.Controllers
{
  public class HomeController : Controller
  {
    private readonly TflApi tflApi = new TflApi();
    private readonly PostcodesApi postcodesApi = new PostcodesApi();

    public ActionResult Index()
    {
      return View();
    }

    [HttpGet]
    public ActionResult BusInfo(PostcodeSelection selection)
    {
      var coordinate = postcodesApi.GetCoordinateForPostcode(selection.Postcode);
      var nearbyStops = tflApi.GetStopsNear(coordinate).Take(2);
      var stopsWithArrivals =
        nearbyStops.Select(
          stop => new StopWithArrivals(stop.CommonName, tflApi.GetArrivalPredictions(stop.NaptanId))).ToList();
      
      var info = new BusInfo(selection.Postcode, stopsWithArrivals);
      return View(info);
    }

    public ActionResult About()
    {
      ViewBag.Message = "Information about this site";

      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Contact us!";

      return View();
    }
  }
}