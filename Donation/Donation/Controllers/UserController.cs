using Donation.Models;
using Donation.Myfolder;
using Donation.Repositary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Razorpay.Api;
using System.Net.Sockets;
using System.Configuration;

namespace Donation.Controllers
{
    public class UserController : Controller
    {
        
      string Str = @"Data Source=RAJESHWARANMURU\SQLEXPRESS01;Initial Catalog=pratice;Integrated Security=True";

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



        //[HttpGet]
        //public ActionResult Show()
        //{
        //    UserImageModel model = new UserImageModel();
        //    return View(model.Show());
        //}

        /// <summary>
        /// return the view of signup form
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult AddDetail()
        {
            return View();
        }

        /// <summary>
        /// call the function for add the details into the database
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult AddDetail(Registration register)
        {
            try
            {
                if (ModelState.IsValid)
                {
                  RegistrationRepositary registerRepo = new RegistrationRepositary();
                    if (registerRepo.AddDetails(register))
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
        /// call the function for to get the details without edit and delete
        /// </summary>
        /// <returns></returns>

        public ActionResult Details()
        {
            RegistrationRepositary registerInfo = new RegistrationRepositary();
            ModelState.Clear();
            return View(registerInfo.GetDetails());
        }




        /// <summary>
        /// Creating a page for user home view page
        /// </summary>
        /// <returns></returns>

        public ActionResult UserPageView()
        {
            return View();
        }
         
        public ActionResult CreateDonation()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Show()
        {
            UserImageModel model = new UserImageModel();
            return View(model.Show());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 

        public ActionResult IndexAddAdminDonation()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(Str))
            {
                con.Open();
                string q = "Select * from admin_donation";
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                da.Fill(dt);

            }
            return View(dt);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            AdminDonation admindonation = new AdminDonation();
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(Str))
            {
                con.Open();
                string q = "Select * from admin_donation where id=" + id;
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                da.Fill(dataTable);
            }
            if (dataTable.Rows.Count == 1)
            {

                admindonation.id = Convert.ToInt32(dataTable.Rows[0][0].ToString());
                admindonation.purpose = dataTable.Rows[0][1].ToString();
                admindonation.description = dataTable.Rows[0][2].ToString();
                admindonation.amount = dataTable.Rows[0][3].ToString();
                admindonation.amount = dataTable.Rows[0][3].ToString();



                return View(admindonation);
            }
            return RedirectToAction("IndexAddAdminDonation");

        }

        public ActionResult ViewAddAdminDonation()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(Str))
            {
                con.Open();
                string q = "Select * from admin_donation";
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                da.Fill(dt);
            }
            return View(dt);

        }

        public ActionResult InitiatePayment(string amount)
        {
            var key = ConfigurationManager.AppSettings["RazorPayKey"].ToString();
            var secret = ConfigurationManager.AppSettings["RazorPaySecret"].ToString();
            RazorpayClient client = new RazorpayClient(key, secret);
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", Convert.ToDecimal(amount) * 100);
            options.Add("currency", "USD");
       
            Order order = client.Order.Create(options);
            ViewBag.orderId = order["id"].ToString();
             return View("Payment");
        }


    }
}