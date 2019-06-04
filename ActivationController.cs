using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookingTable.Business;
using BookingTable.Business.IRepository;
using BookingTable.Business.Repository;
using BookingTable.Entities.Entities;
using BookingTable.Entities.Models;

namespace BookingTable.Web.Controllers
{
    public class ActivationController : Controller
    {
        // GET: Activation
        private readonly ICustomerRepository _customerRepository = new CustomerRepository();

      

        public ActionResult Activation()
        {
            ViewBag.Message = "Invalid Activation code.";
            if (RouteData.Values["id"] != null)
            {
                Guid activationCode = new Guid(RouteData.Values["id"].ToString());

                var UserActivation = _customerRepository.GetActivation(activationCode);

               

                //UserActivation userActivation = usersEntitie.UserActivations.Where(p => p.ActivationCode == activationCode).FirstOrDefault();
                if (UserActivation != null)
                {
                   
                    ViewBag.Message = "Activation successful.";
                    
                }
            }

            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: Activation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Activation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Activation/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Activation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Activation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Activation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Activation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
