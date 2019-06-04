using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookingTable.Business.IRepository;
using BookingTable.Business.Repository;
using BookingTable.Entities.Enum;
using BookingTable.Web.Business;
using BookingTable.Web.Helpers;

namespace BookingTable.Web.Controllers
{
    public class OfferController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            var foods = GetSelectList.GetOffers();

            if (foods != null)
            {
                ViewBag.ModelhasValue = "True";
            }
            return View();
        }
    }
}