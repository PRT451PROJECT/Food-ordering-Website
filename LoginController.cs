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
using BookingTable.Entities.Enum;
using BookingTable.Web.Helpers;
using BookingTable.Web.Security;
using System.Net.Mail;
using System.Net;

namespace BookingTable.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ICustomerRepository _customerRepository = new CustomerRepository();

        //GET
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Remove("User");
            Session.Remove("ReturnToUrl");
            Session.Remove("Booking");
            Session.Remove("BookingEntry");
            return RedirectToAction("Index", "Home");
        }

        //POST
        [HttpPost]
        //[UserAuthorizedAttribute]
        public ActionResult Login(LoginModel model)
        {
            var message = new MessageModel
            {
                Content = Resources.Resources.Message_Success_Login,
                Title = Resources.Resources.Content_Success,
                Type = MessageTypeEnum.SuccessReload.ToString(),
                ClosePopup = true
            };
            if (model != null)
            {
                var entity = _customerRepository.Login(model.Username, Utils.ToMd5Hash(model.Password));

                if (entity != null)
                {
                    Session["User"] = entity;
                    message.Content = string.Format(Resources.Resources.Content_HelloOldMember, entity.FullName);
                    AdminAuthorizedAttribute admin = new AdminAuthorizedAttribute();
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
                message = new MessageModel
                {
                    Content = Resources.Resources.Message_Error_UsernameNotExist,
                    Title = Resources.Resources.Content_Error,
                    Type = MessageTypeEnum.Error.ToString(),
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            message = new MessageModel
            {
                Content = Resources.Resources.Message_Error_Validate,
                Title = Resources.Resources.Content_Error,
                Type = MessageTypeEnum.Error.ToString()
            };

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        //POST
        [HttpPost]
        //[UserAuthorizedAttribute]
        public ActionResult GuesstLogin(LoginModel model)
        {
            var message = new MessageModel
            {
                Content = Resources.Resources.Message_Success_Login,
                Title = Resources.Resources.Content_Success,
                Type = MessageTypeEnum.SuccessReload.ToString(),
                ClosePopup = true
            };
            if (model != null)
            {
                var entity = _customerRepository.Login("GuestUser", Utils.ToMd5Hash("123456"));

                if (entity != null )
                {
                    Session["User"] = entity;
                    message.Content = string.Format(Resources.Resources.Content_HelloOldMember, entity.FullName);
                    AdminAuthorizedAttribute admin = new AdminAuthorizedAttribute();
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
                message = new MessageModel
                {
                    Content = Resources.Resources.Message_Error_UsernameNotExist,
                    Title = Resources.Resources.Content_Error,
                    Type = MessageTypeEnum.Error.ToString(),
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            message = new MessageModel
            {
                Content = Resources.Resources.Message_Error_Validate,
                Title = Resources.Resources.Content_Error,
                Type = MessageTypeEnum.Error.ToString()
            };

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
                
        public ActionResult Register(UserModel model)
        {
            if (model == null) { return View(); }
            var message = new MessageModel
            {
                Content = Resources.Resources.Message_Success_Register,
                Title = Resources.Resources.Content_Success,
                Type = MessageTypeEnum.SuccessReload.ToString(),
                ClosePopup = true
            };

            //Parse
            var entity = new Customer()
            {
                Id = model.Id,
                DateOfBirth = model.DateOfBirth,
                Username = model.Username,
                Password = model.Password,
                Phone = model.Phone,
                Email = model.Email,
                FullName = model.FullName,
                Address = model.Address,
                Active = true,
                Deleted = false,
                LastUpdate = DateTime.Now,
            };

            //Check Password = Password confirm
            if (model.Password != model.PasswordConfirm)
            {
                message = new MessageModel
                {
                    Content = Resources.Resources.Message_Error_PasswordConfirm,
                    Title = Resources.Resources.Content_Error,
                    Type = MessageTypeEnum.Error.ToString()
                };

                return Json(message, JsonRequestBehavior.AllowGet);
            }

            //Check username Exist
            if (!_customerRepository.IsValid(entity))
            {
                message = new MessageModel
                {
                    Content = Resources.Resources.Message_Error_UserExisted,
                    Title = Resources.Resources.Content_Error,
                    Type = MessageTypeEnum.Error.ToString()
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            entity.Password = Utils.ToMd5Hash(entity.Password);

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
            SendActivationEmail(entity);
            Session["User"] = entity;
            message.Content = string.Format("Registration successful, mail send to your mail id", entity.FullName,
                WebSetting.GetWebContent().WebLongName);
            Session["ReturnToUrl"] = "Home/Index";
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        private void SendActivationEmail(Customer user)
        {
            Guid activationCode = Guid.NewGuid();
            

            var usersEntities = _customerRepository.Find(user.Id);

            usersEntities.ActivationCode = activationCode;

            var entity = _customerRepository.Save(usersEntities);

            using (MailMessage mm = new MailMessage("chaitra4kk@gmail.com", user.Email))
            {
                mm.Subject = "Account Activation";
                string body = "Hello " + user.Username + ",";
                body += "<br /><br />Please click the following link to activate your account";
                body += "<br /><a href = '" + string.Format("{0}://{1}/Login/Activation/{2}", Request.Url.Scheme, Request.Url.Authority, activationCode) + "'>Click here to activate your account.</a>";
                body += "<br /><br />Thanks";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("chaitra4kk@gmail.com", "chaithrahari4");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }
        public ActionResult Activation()
        {
            var message = new MessageModel
            {
                Content = Resources.Resources.Message_Success_Login,
                Title = Resources.Resources.Content_Success,
                Type = MessageTypeEnum.SuccessReload.ToString(),
                ClosePopup = true
            };

            ViewBag.Message = "Invalid Activation code.";
            if (RouteData.Values["id"] != null)
            {
                Guid activationCode = new Guid(RouteData.Values["id"].ToString());

                var UserActivation = _customerRepository.GetActivation(activationCode);



                //UserActivation userActivation = usersEntitie.UserActivations.Where(p => p.ActivationCode == activationCode).FirstOrDefault();
                if (UserActivation != null)
                {

                    ViewBag.Message = "Activation successful. Please sing-in";
                    return View("Index");
                }
            }

            return View();
        }

    }
}