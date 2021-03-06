﻿using System.Web.Mvc;
using BusBoard.Web.Models;
using BusBoard.Web.ViewModels;
using BusBoard.Api;
using System;

namespace BusBoard.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BusInfo(PostcodeSelection selection)
        {
            // Add some properties to the BusInfo view model with the data you want to render on the page.
            // Write code here to populate the view model with info from the APIs.
            // Then modify the view (in Views/Home/BusInfo.cshtml) to render upcoming buses

            // Validate submitted postcode
            PostcodeAPI pc = new PostcodeAPI();
            bool isPcValid = pc.ValidPostcode(selection.Postcode);

            // populate info object to be passed to view
            var info = new Web.ViewModels.BusInfo()
            {
                PostCode = (selection.Postcode == null) ? "" : selection.Postcode.ToUpper(),

                IsPostcodeValid = isPcValid,

                NumberOfStops = (selection.NumberOfStops == null) ? "All" : selection.NumberOfStops,
            };

            // Get arrival info by submitted postcode, only if postcode is valid
            if (isPcValid)
                info.Stops = (selection.NumberOfStops == null) ?
                    Api.BusStop.GetBusStopArrivalsByPostcode(selection.Postcode) :
                    Api.BusStop.GetBusStopArrivalsByPostcode(selection.Postcode, Convert.ToInt32(selection.NumberOfStops));

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