using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BookingTable.Business.IRepository;
using BookingTable.Business.Repository;
using BookingTable.Entities.Enum;
using BookingTable.Entities.Models;
using BookingTable.Web.Business;
using BookingTable.Web.Helpers;
using BookingTable.Web.Security;




namespace BookingTable.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomerRepository _customerRepository = new CustomerRepository();
        // GET: Home
        
        public ActionResult Index()
        {
            Session.Remove("ReturnToUrl");
            IFoodRepository foodRepository = new FoodRepository();
            ISettingRepository settingRepository = new SettingRepository();
            IOrdersRepository ordersrepository = new OrdersRepository();
            var orders = ordersrepository.GetOrders();
            Session["ReturnToUrl"] = "Home/Index";
            if (orders.Count > 0)
            {
                return View(orders);
            }
            return null;

        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
     
       
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}