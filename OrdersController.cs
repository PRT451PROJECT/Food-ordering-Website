using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BookingTable.Business.IRepository;
using BookingTable.Business.Repository;
using BookingTable.Entities.Entities;
using BookingTable.Entities.Enum;
using BookingTable.Entities.Models;
using BookingTable.Entities.Paypal;
using BookingTable.Web.Business;
using BookingTable.Web.Helpers;
using BookingTable.Web.Security;
using Microsoft.Build.Tasks;
using Order = BookingTable.Entities.Entities.Order;

namespace BookingTable.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ITableRepository _tableRepository = new TableRepository();
        private readonly IFoodRepository _foodRepository = new FoodRepository();
        private readonly IOrdersRepository _ordersRepository = new OrdersRepository();

        // GET
        public ActionResult Booking()
        {
            if (Session["BookingEntry"] != null)
            {
                return PartialView("_Booking");
            }
            return null;
        }
        [UserAuthorizedAttribute]
       
        public ActionResult ViewBooking()
        {
            return View("ViewBooking");
        }

        [UserAuthorized]
        public ActionResult SuccessPayment()
        {
            ISettingRepository settingRepository = new SettingRepository();

            var pdtToken = settingRepository.GetSettingByKey(PaypalSettingEnum.PDTToken.ToString());
            var mode = settingRepository.GetSettingByKey(PaypalSettingEnum.Mode.ToString());
            var paypalUrl = settingRepository.GetSettingByKey(mode.Value);

           // var payment = PDTHolder.Success(Request.QueryString.Get("tx"), pdtToken.Value, paypalUrl.Value);
            var order = Session["Booking"] as Order;
           

            if (order != null)
            {
               
                if (order != null && order.Id == 0)
                {
                    var subTotal = order.DepositPrice.Value + order.SubTotal;
                    var discount = order.Discount.Value / 100 * subTotal;
                    var tax = order.Tax.Value / 100 * (subTotal - discount);
                    var total = subTotal - discount + tax;
                    if (Math.Round(double.Parse(total.ToString()))>0)
                    {
                        var entity = new Order
                        {
                            CustomerId = order.CustomerId,
                            CreatorId = order.CreatorId,
                            LastUpdate = DateTime.Now,
                            CreationTime = DateTime.Now,
                            Name = order.Name,
                            Note = order.Note,
                            DepositPrice = total,
                            SubTotal = order.SubTotal,
                            Tax = order.Tax,
                            Discount = order.Discount,
                            OrderDetails = new List<OrderDetail>(),
                        };
                        foreach (var item in order.OrderDetails)
                        {
                            entity.OrderDetails.Add(new OrderDetail
                            {
                                CreatorId = item.CreatorId,
                                TableId = 9,
                                FoodId = item.FoodId,
                                FoodPrice = item.FoodPrice,
                                Quantity = item.Quantity,
                                Subtotal = item.Subtotal,
                                CreationTime = DateTime.Now,
                                OrderTime = item.OrderTime,
                                LastUpdate = DateTime.Now,
                            });
                        }
                      
                        entity.Note += "\nPayment info:\n\tFull Name: " + order.Customer.FullName + " " + "\n\tEmail: " + order.Customer.Email + "\n\tGross total: " +
                                       total.ToString("c", new CultureInfo("en-AU"));
                        if (!_ordersRepository.Save(entity))
                        {
                            return RedirectToAction("Error", "Home");
                        }
                        Session["ReturnToUrl"] = "Orders/ViewBooking";
                        order.Id = entity.Id;
                        Session.Remove("Booking");
                        Session.Remove("BookingEntry");
                    }
                }
                
                Session["Booking"]=order;
            }
            return View(order);
        }

        public ActionResult DeleteTable(int id)
        {
            return PartialView("_DeleteTable", _tableRepository.Find(id));
        }
        public ActionResult DeleteFood(int id)
        {
            return PartialView("_DeleteFood", _foodRepository.Find(id));
        }

        public ActionResult ClearBooking()
        {
            return PartialView("_ClearBooking");
        }
         [HttpGet]
        public ActionResult Search()
        {
            
            return View();
        }
        //POST
        [HttpPost]
         public ActionResult Search(FormCollection form)
        {
            if (string.IsNullOrEmpty(form["Search"] ))
            {
                RedirectToAction("Index", "Home");
            }
           string searchResult = form["Search"];

           var food = _foodRepository.SearchFoodName(searchResult);
           Session["FoodEntry"] = food;
           ViewBag.Search = searchResult;
           return View();
        }

        public ActionResult PrintInvoice(int orderId)
        {
            var entity = _ordersRepository.Find(orderId);
            if (entity.Completed != true)
            {
                //Pay
                foreach (var item in entity.OrderDetails)
                {
                    item.Completed = true;
                }
                entity.Completed = true;
                entity.PaymentTime = DateTime.Now;
                entity.PayeeId = 1;
                _ordersRepository.Save(entity);
            }
            return View(entity);
        }

        [HttpPost]
        public int BookingCart(OrderDetail model)
        {
            try
            {
               
                if (model != null)
                {
                    var cart = Session["BookingEntry"] as Order;


                    //var orderTime = DateTime.ParseExact("18/04/2019 11:10:00", "dd/MM/yyyy hh:mm tt",
                    //    CultureInfo.InvariantCulture);

                    var orderTime = DateTime.Now;

                    var food = _foodRepository.Find(model.Id);


                    if (cart == null)
                    {
                        cart = new Order
                        {

                            OrderDetails = new List<OrderDetail>
                                {
                                    new OrderDetail
                                    {
                                       FoodId = model.Id, 
                                       FoodPrice = food.Price,
                                       OrderTime = orderTime,
                                       FoodName=food.Name, 
                                       TableId = 9,
                                       Quantity = food.Quantity,
                                       Subtotal = food.Price * food.Quantity,
                                       CreationTime = DateTime.Now,       
                                       LastUpdate = DateTime.Now
                                      
                                    }
                                }
                            

                        };
                    }
                    else
                    {
                        if (!cart.OrderDetails.Any(x => x.TableId == model.Id && x.OrderTime.Value.Date == orderTime.Date && x.OrderTime.Value.Hour == orderTime.Hour))
                        {
                            cart.OrderDetails.Add(new OrderDetail
                            {
                                FoodId = model.Id,                                
                                FoodPrice = food.Price,
                                OrderTime = orderTime,
                                FoodName = food.Name,
                                TableId = 9,
                                Quantity = food.Quantity,
                                Subtotal = food.Price * food.Quantity,
                                CreationTime = DateTime.Now,       
                                LastUpdate = DateTime.Now
                            });

                        }
                        else
                        {
                            cart.OrderDetails.Remove(cart.OrderDetails.First(x => x.TableId == model.Id));

                        }
                    }

                    if (DateTime.Today.AddDays(int.Parse(WebSetting.GetBookingLimit())) < model.OrderTime)
                    {
                        RedirectToAction("Index", "Home");
                    }

                    ISettingRepository settingRepository = new SettingRepository();
                    var discount = decimal.Parse(settingRepository.GetSettingByKey(SystemSettingEnum.Discount.ToString()).Value);
                    var tax = decimal.Parse(settingRepository.GetSettingByKey(SystemSettingEnum.Tax.ToString()).Value);
                    var order = new Order
                    {
                        LastUpdate = DateTime.Now,
                        CreationTime = DateTime.Now,
                        Name = Resources.Resources.Content_Booking,
                       // Note = model.Note,
                        DepositPrice = 0,
                        Discount = discount,
                        Tax = tax,
                        OrderDetails = new List<OrderDetail>(),
                    };

              
                        if (cart.OrderDetails != null)
                        {
                            for (var i = 0; i < cart.OrderDetails.Count; i++)
                            {
                                cart.OrderDetails.ElementAt(i).Id = i + 1;
                            }
                        }
                        Session["BookingEntry"] = cart;
                        return cart.OrderDetails.Count;
                    
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return 1;

        }
        [HttpPost]
   
        public ActionResult SubmitBooking(Order model)
        {
            if (DateTime.Today.AddDays(int.Parse(WebSetting.GetBookingLimit())) < model.OrderTime)
            {
                RedirectToAction("Index", "Home");
            }

            ISettingRepository settingRepository = new SettingRepository();
            var discount = decimal.Parse(settingRepository.GetSettingByKey(SystemSettingEnum.Discount.ToString()).Value);
            var tax = decimal.Parse(settingRepository.GetSettingByKey(SystemSettingEnum.Tax.ToString()).Value);
            var order = new Order
            {
                LastUpdate = DateTime.Now,
                CreationTime = DateTime.Now,
                Name = Resources.Resources.Content_Booking,
                Note = model.Note,
                DepositPrice = 0,
                Discount = discount,
                Tax = tax,
                OrderDetails = new List<OrderDetail>(),
            };

            var tableBll = new TableBll();
            foreach (var item in model.OrderDetails)
            {
                if (tableBll.GetOrderByTableIdAndTime(item.TableId, item.OrderTime.Value) != null)
                {
                    Session.Remove("BookingEntry");
                    RedirectToAction("Index", "Home");
                }

                

                if (!item.FoodId.HasValue)
                {
                    var food = _foodRepository.Find(item.FoodId.Value);

                    order.OrderDetails.Add(new OrderDetail
                    {
                            TableId = 9,   
                            FoodId = item.FoodId,
                            FoodName = food.Name,
                            FoodPrice =food.Price,
                            Quantity = food.Quantity,
                            Subtotal = food.Price * food.Quantity,
                            CreationTime = DateTime.Now,
                            OrderTime = item.OrderTime,
                            LastUpdate = DateTime.Now
                    });
                  
                }
                else
                {

                    var food = _foodRepository.Find(item.FoodId.Value);

                    if (!item.Quantity.HasValue || !(item.Quantity > 0) || food.Quantity < item.Quantity.Value)
                        return RedirectToAction("Error", "Home");

                    var sameOrderdetails = order.OrderDetails.FirstOrDefault(x => x.FoodId == food.Id && x.TableId == item.TableId);
                    if (sameOrderdetails == null)
                    {
                        order.OrderDetails.Add(new OrderDetail
                        {
                            TableId = item.TableId,
                          
                            Food = food,
                            FoodId = food.Id,
                            FoodName = food.Name,
                            FoodPrice = food.Price,
                            Quantity = item.Quantity,
                            Subtotal = food.Price * item.Quantity.Value,
                            CreationTime = DateTime.Now,
                            LastUpdate = DateTime.Now
                        });
                    }
                    else
                    {
                        sameOrderdetails.Quantity++;
                    }
                    order.SubTotal += food.Price * item.Quantity.Value;
                    //Session["ReturnToUrl"] = "Orders/viewbooking";
                }
            }
          
            Session["Booking"] = order;

            return RedirectToAction("ViewBooking");
        }


        [HttpPost]
        public ActionResult DeleteFood(Food food)
        {
            var order = Session["Booking"] as Order;

            foreach(var item in order.OrderDetails.Where(x => x.FoodId == food.Id).ToList())
            {
                order.OrderDetails.Remove(item);
            }
            order.DepositPrice = 0;
            order.SubTotal = 0;
            foreach (var item in order.OrderDetails)
            {
                if (!item.FoodId.HasValue)
                {
                    order.DepositPrice += item.Table.TableType.DepositPrice;
                }
                else
                {
                    order.SubTotal += item.Food.Price * item.Quantity.Value;
                }
            }
            Session["Booking"] = order;
            return RedirectToAction("ViewBooking");
        }
        [HttpPost]
        public ActionResult DeleteTable(Table table)
        {
            var tables = Session["BookingEntry"] as Order;
            foreach (var item in tables.OrderDetails.Where(x => x.TableId == table.Id).ToList())
            {
                tables.OrderDetails.Remove(item);
            }
            Session["BookingEntry"] = tables;
            var order = Session["Booking"] as Order;

            foreach (var item in order.OrderDetails.Where(x => x.TableId == table.Id).ToList())
            {
                order.OrderDetails.Remove(item);
            }
            order.DepositPrice = 0;
            order.SubTotal = 0;
            foreach (var item in order.OrderDetails)
            {
                if (!item.FoodId.HasValue)
                {
                    order.DepositPrice += item.Table.TableType.DepositPrice;
                }
                else
                {
                    order.SubTotal += item.Food.Price * item.Quantity.Value;
                }
            }
            if (order.OrderDetails.Count == 0)
            {
                Session.Remove("Booking");
                return RedirectToAction("Index","Home");
            }
                Session["Booking"] = order;
            
            return RedirectToAction("ViewBooking");
        }
        [HttpPost]
        public ActionResult ClearAndRebook()
        {
            Session.Remove("Booking");
            Session.Remove("BookingEntry");

            return RedirectToAction("Index", "Home");
        }
    }
}