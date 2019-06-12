using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Project_Builder_Development.Models;
using System.Data.Entity.Validation;
using Microsoft.AspNet.Identity;

namespace Project_Builder_Development.Controllers
{
    public class UserController : Controller
    {

        //Registration Action
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        //Registration POST Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified, ActivationCode")] User user)
        {
            bool Status = false;
            string message = "";

            //Model Validation
            if (ModelState.IsValid)
            {

                //Email is already Exist
                #region Email is already Exist
                var isExist = IsEmailExist(user.EmailID);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(user);
                }

                #endregion

                //Project Builder ID is already Exist
                #region Project Builder ID is already Exist
                var isIdExist = IsPBIDExist(user.PBID);
                if (isIdExist)
                {
                    ModelState.AddModelError("PBIDExist", "Project Builder ID already exist");
                    return View(user);
                }
                #endregion


                //Generate Activation Code
                #region Generate Activation Code
                user.ActivationCode = Guid.NewGuid();
                #endregion

                //Password Hashing
                #region Password Hashing
                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);
                #endregion
                user.IsEmailVerified = false;

                //Save data to Database
                #region Save data to Database
                using (BuilderDatabaseEntities dc = new BuilderDatabaseEntities())
                {
                    dc.Users.Add(user);
                    dc.Configuration.ValidateOnSaveEnabled = false;
                    dc.SaveChanges();

                    //Send Email to User
                    SendVerificationLinkEmail(user.EmailID, user.ActivationCode.ToString());
                    message = "Registration successfully done. Account activation link " +
                        " has been sent to your email id:" + user.EmailID;
                    Status = true;
                }
                #endregion
            }
            else
            {
                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);

        }

        //Verify Account
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (BuilderDatabaseEntities dc = new BuilderDatabaseEntities())
            {
                // This line I have added here to avoid confirm pasword does not match issue
                dc.Configuration.ValidateOnSaveEnabled = false;

                var v = dc.Users.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.IsEmailVerified = true;
                    dc.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }
            ViewBag.Status = Status;
            return View();
        }

        //Login
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        //Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string ReturnUrl = "")
        {
            string message = "";
            using (BuilderDatabaseEntities dc = new BuilderDatabaseEntities())
            {
                var v = dc.Users.Where(a => a.EmailID == login.EmailID).FirstOrDefault();
                if (v != null)
                {
                    if (string.Compare(Crypto.Hash(login.Password), v.Password) == 0)
                    {
                        // 525600 min = 1 year
                        int timeout = login.RememberMe ? 525600 : 20;
                        var ticket = new FormsAuthenticationTicket(login.EmailID, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        string logName = login.EmailID;
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);


                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                            //return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }

                    }

                    else
                    {
                        message = "Invalid credential provided";
                    }
                }
                else
                {
                    message = "Invalid credential provided";
                }
            }

            ViewBag.Message = message;
            return View();



        }

        //Logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            using (BuilderDatabaseEntities dc = new BuilderDatabaseEntities())
            {
                var v = dc.Users.Where(a => a.EmailID == emailID).FirstOrDefault();
                return v != null;
            }
        }

        [NonAction]
        public bool IsPBIDExist(string pbID)
        {
            using (BuilderDatabaseEntities dc = new BuilderDatabaseEntities())
            {
                var v = dc.Users.Where(a => a.PBID == pbID).FirstOrDefault();
                return v != null;
            }
        }

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "VerifyAccount")
        {
            // var scheme = Request.Url.Scheme;
            // var host = Request.Url.Host;
            // var port = Request.Url.Port;

            // string url = scheme + "://" + host +

            var verifyUrl = "/User/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("prj.projectbuilder@gmail.com", "Project Builder");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Builder666!";

            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "Your account is successfully created!";

                body = "<br/><br/>We are exited to tell you that your account is " +
                    " successfully created. Please click on the below link to verify your account" +
                    " <br/><br/><a href='" + link + "'>" + link + "</a> ";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";

                body = "Hi, <br/><br/>We got request for reset your account password. Please check on the below link to reset your password" +
                       "<br/><br/><a href=" + link + ">Reset Password link</a>";

            }



            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })

                smtp.Send(message);
        }

        //Forgot Password
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string EmailID)
        {
            //Verify Email ID

            //Generate Reset password link

            //Send Email

            string message = "";
            bool status = false;

            using (BuilderDatabaseEntities dc = new BuilderDatabaseEntities())
            {
                var account = dc.Users.Where(a => a.EmailID == EmailID).FirstOrDefault();
                if (account != null)
                {
                    //Send email for reset password
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(account.EmailID, resetCode, "ResetPassword");
                    account.ResetPasswordCode = resetCode;

                    //This link I have added here to avoid confirm password not match issue, as we had added a confirm password property
                    //in our model in register
                    dc.Configuration.ValidateOnSaveEnabled = false;
                    dc.SaveChanges();
                    message = "Reset password link has been sent to your email id.";

                }
                else
                {
                    message = "Account not found";
                }
            }
            ViewBag.Message = message;
            return View();
        }


        public ActionResult ResetPassword(string id)
        {
            //Verify the reset password link
            //Find account associated with this link
            //redirect to reset password page
            using (BuilderDatabaseEntities dc = new BuilderDatabaseEntities())
            {
                var user = dc.Users.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using (BuilderDatabaseEntities dc = new BuilderDatabaseEntities())
                {
                    var user = dc.Users.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        user.Password = Crypto.Hash(model.NewPassword);
                        user.ResetPasswordCode = "";

                        dc.Configuration.ValidateOnSaveEnabled = false;
                        dc.SaveChanges();
                        message = "New password updated successfully";
                    }
                }
            }
            else
            {
                message = "Something Invalid";
            }
            ViewBag.Message = message;
            return RedirectToAction("Index", "Home");
        }


        // GET: ChangePassword
        public ActionResult ChangePassword()
        {
            return View();

        }


        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswrodModel model)
        {
            var message = "";

            model.EmailID = User.Identity.Name;

            using (BuilderDatabaseEntities dc = new BuilderDatabaseEntities())
            {
                var user = dc.Users.Where(a => a.EmailID == model.EmailID).FirstOrDefault();
                if (user != null)
                {
                    if (string.Compare(Crypto.Hash(model.OldPassword), user.Password) == 0)
                    {
                        user.Password = Crypto.Hash(model.NewPassword);
                        user.ResetPasswordCode = "";

                        dc.Configuration.ValidateOnSaveEnabled = false;
                        dc.SaveChanges();
                        message = "Password changed successfully";
                    }
                    else
                    {
                        message = "Password ERROR";
                    }
                }
                else
                {
                    message = "USER ERROR";
                }

            }

            ViewBag.Message = message;
            return RedirectToAction("Index", "Home");
        }

       

    }
}