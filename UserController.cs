using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookingTable.Business;
using BookingTable.Business.IRepository;
using BookingTable.Business.Repository;
using BookingTable.Entities.Entities;
using BookingTable.Entities.Enum;
using BookingTable.Web.Helpers;
using BookingTable.Entities.Models;
using BookingTable.Web.Security;

namespace BookingTable.Web.Controllers
{
    [UserAuthorized]
    public class UserController : Controller
    {
        private readonly ICustomerRepository _customerRepository = new CustomerRepository();
        // GET: Customer

        public ActionResult Profile()
        {
            var entity = _customerRepository.Find(Support.GetCurrentUser().Id);
            return View(entity);
        }
        public ActionResult ChangePassword()
        {
            var model = new ChangePasswordModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateProfile(DateTime dateOfBirth,string phone, string email, string fullName, string address, HttpPostedFileBase image)
        {

            var message = new MessageModel
            {
                Content = Resources.Resources.Message_Success_Update_Profile,
                Title = Resources.Resources.Content_Success,
                Type = MessageTypeEnum.SuccessReload.ToString(),
                ClosePopup = true
            };

            var entity = Support.GetCurrentUser();

            entity.DateOfBirth = dateOfBirth;
            entity.Phone = phone;
            entity.Email = email;
            entity.FullName = fullName;
            entity.Address = address;
            entity.LastUpdate = DateTime.Now;

            //Validate
            if (!Validator.Validate(entity))
            {
                message = new MessageModel
                {
                    Content = Resources.Resources.Message_Error_Validate,
                    Title = Resources.Resources.Content_Error,
                    Type = MessageTypeEnum.Error.ToString()
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            //Action
            entity.LastUpdate = DateTime.Now;
            //Save
            if (!_customerRepository.Save(entity))
            {
                message = new MessageModel
                {
                    Content = Resources.Resources.Message_Error_System,
                    Title = Resources.Resources.Content_Error,
                    Type = MessageTypeEnum.Error.ToString()
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            //Save image
            if (image != null)
            {
                entity.Image = Support.SavePhoto(image, "customer_" + entity.Id, Server.MapPath("~/Content/Uploads/Photos"));
                _customerRepository.Save(entity);
            }

            Session["User"] = entity;

            //Return
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            var message = new MessageModel
            {
                Content = Resources.Resources.Message_Success_Update_Password,
                Title = Resources.Resources.Content_Success,
                Type = MessageTypeEnum.SuccessReload.ToString(),
                ClosePopup = true
            };
            if (model.New != model.Confirm)
            {
                message = new MessageModel
                {
                    Content = Resources.Resources.Message_Error_PasswordConfirm,
                    Title = Resources.Resources.Content_Error,
                    Type = "error"
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            var entity = _customerRepository.Find(Support.GetCurrentUser().Id);
            if (entity == null)
            {
                message = new MessageModel
                {
                    Content = Resources.Resources.Message_Error_Validate,
                    Title = Resources.Resources.Content_Error,
                    Type = MessageTypeEnum.Error.ToString()
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            if (entity.Password != Utils.ToMd5Hash(model.Old))
            {
                message = new MessageModel
                {
                    Content = Resources.Resources.Massage_Error_PasswordNotMatch,
                    Title = Resources.Resources.Content_Error,
                    Type = MessageTypeEnum.Error.ToString()
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            entity.Password = Utils.ToMd5Hash(model.New);
            entity.UpdateByAdminId = Support.GetCurrentAdmin().Id;
            entity.LastUpdate = DateTime.Now;

            if (!_customerRepository.Save(entity))
            {
                message = new MessageModel
                {
                    Content = Resources.Resources.Message_Error_System,
                    Title = Resources.Resources.Content_Error,
                    Type = MessageTypeEnum.Error.ToString()
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }


            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}