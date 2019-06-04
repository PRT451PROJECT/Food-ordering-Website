using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookingTable.Business.IRepository;
using BookingTable.Business.Repository;
using BookingTable.Entities.Entities;

namespace BookingTable.Web.Controllers
{
   

    public class FeedBackController : Controller
    {
        private readonly IFeedBackRepository _feedbackRepository = new FeedBackRepository();
        // GET: FeedBack
        public ActionResult Index()
        {
            return View();
        }

        // GET: Articles/Details/5
        public ActionResult Details()
        {
            
            

            var feedback = _feedbackRepository.GetFeedBack().SingleOrDefault();

            if (feedback == null)
            {
                return HttpNotFound();
            }
          
            var comments = _feedbackRepository.GetFeedBack();

            //var comments = db.ArticlesComments.Where(d => d.ArticleId.Equals(id.Value)).ToList();
            ViewBag.Comments = comments;

            var ratings = _feedbackRepository.GetFeedBack(); 
           
            ViewBag.RatingSum = 0;
            ViewBag.RatingCount = 0;
           

            return View(feedback);
        }

        // GET: FeedBack/Create
        public ActionResult Create()
        {
            var feedback = _feedbackRepository.GetFeedBack();

            if (feedback == null)
            {
                return HttpNotFound();
            }

            var comments = _feedbackRepository.GetFeedBack();

            //var comments = db.ArticlesComments.Where(d => d.ArticleId.Equals(id.Value)).ToList();
            ViewBag.Comments = comments;

            var ratings = _feedbackRepository.GetFeedBack();
            if (ratings.Count() > 0)
            {
               
                var ratingSum = ratings.Sum(d => Convert.ToInt32(d.Rating));
                ViewBag.RatingSum = ratingSum;
                
                var ratingCount = ratings.Count();
                ViewBag.RatingCount = ratingCount;
            }
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
            }

            return View(feedback);
        }

        // POST: FeedBack/Create
       
        [HttpPost]
        public ActionResult Save(FeedBack entity, String Comment)
        {
            try
            {
                entity.Comments = Comment;
                entity.CommentsDate = DateTime.Now;
                // TODO: Add insert logic here
                if (!_feedbackRepository.Save(entity))
                {
                    return RedirectToAction("Error", "Home");
                }
                var feedback = _feedbackRepository.GetFeedBack();
                
                
                return View("Create",feedback);
            }
            catch
            {
                return View();
            }
        }

        // GET: FeedBack/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FeedBack/Edit/5
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

        // GET: FeedBack/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FeedBack/Delete/5
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
