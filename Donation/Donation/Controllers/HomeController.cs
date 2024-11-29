using Donation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Donation.Repositary;
using Donation.Myfolder;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;

namespace Donation.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        /// <summary>
        /// Return a page of user message in a contact
        /// </summary>
        /// <returns>Contact page</returns>

        [AllowAnonymous]
        public ActionResult AddContactDetail()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult AddContactDetail(ContactModel contact)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    ContactRepositary contactRepository = new ContactRepositary();
                    if (contactRepository.AddContact(contact))
                    {
                        ViewBag.Message = "User details Added Successfully";
                    }
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message);
                ViewBag.Message = "name is invalid";
                return View();
            }
        }




        /// <summary>
        /// return the view of signup form
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult AddDetail()
        {
            return View();
        }

        /// <summary>
        /// call the function for add the details into the database
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult AddDetail(Registration register)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RegistrationRepositary regRepo = new RegistrationRepositary();
                    if (regRepo.AddDetails(register))
                    {
                        ViewBag.Message = "User details Added Successfully";
                    }
                }

                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message);
                ViewBag.Message = "User name is invalid";
                return View();
            }
        }


        /// <summary>
        /// Returns a login page for user
        /// </summary>
        /// <returns>~/Home/Login</returns>
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Code for user login and authenticate the roles and redirect the page accordingly
        /// </summary>
        /// <param name="loginObj"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginMod loginObj)
        {
            Password EncryptData = new Password();
            string mainconnection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(mainconnection);
            string sqlquery = "select Email,Password from [dbo].[main] where Email=@Email and Password=@password";
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlquery, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Email", loginObj.Email);
            sqlCommand.Parameters.AddWithValue("@password", EncryptData.Encode(loginObj.password));
            SqlDataReader sqldata = sqlCommand.ExecuteReader();

            //string queryValidate = "SELECT COUNT(Username) FROM UserProfile WHERE Username=@username";

            //DataTable objdt = new DataTable();
            //SqlDataAdapter da = new SqlDataAdapter(queryValidate, sqlCon);
            //da.Fill(objdt);

            //while(sqldata.Read())
            //{
            //    if (sqldata["Username"].ToString() .Equals( loginObj.Username))
            //    {
            //        ViewData["Message"] = "User Login Details Failed";
            //        return View();
            //    }
            //}

            //if(queryValidate.Length > 0)
            //{
            //    ViewData["Message"] = "User Login Details Failed";
            //}

            try
            {
                if (ModelState.IsValid)
                {
                    //TempData["UserName"] = loginObj.Username;
                    Session.Add("Email", loginObj.Email);

                    try
                    {
                        try
                        {
                            if (Membership.ValidateUser(loginObj.Email, loginObj.password))
                            {
                                FormsAuthentication.SetAuthCookie(loginObj.Email, false);
                                /* return RedirectToAction("UserPageView", "User"); */
                                return RedirectToAction("About", "User");

                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLog(ex.Message);
                        }
                        //else
                        //{
                        //    ViewData["Message"] = "User login Details Failed";
                        //}
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex.Message);
                    }

                    if (loginObj.Email == "admin" && loginObj.password == "admin123")
                    {
                        FormsAuthentication.SetAuthCookie(loginObj.Email, false);
                        return RedirectToAction("AdminPageView", "Admin");
                    }
                    else
                    {
                        ViewBag.Message = "Admin login Details Failed";
                    }
                }
            }

            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message);
            }

            if (sqldata.Read())
            {
                return RedirectToAction("UserPageView", "User");
            }


            Response.Redirect(Request.Url.ToString(), false);

            sqlConnection.Close();
            return View();
        }

    }
}