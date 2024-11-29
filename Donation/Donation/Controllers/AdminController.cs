using DocumentFormat.OpenXml.EMMA;
using Donation.Models;
using Donation.Myfolder;
using Donation.Repositary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Donation.Controllers
{
    public class AdminController : Controller
    {
        string Str = @"Data Source=RAJESHWARANMURU\SQLEXPRESS01;Initial Catalog=pratice;Integrated Security=True";


        /// <summary>
        /// returns the home page of admin
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
            public ActionResult AdminPageView()
            {
                return View();
            }

        /// <summary>
        /// returns the page to add a template in user page
        /// </summary>
        /// <returns></returns>

        public ActionResult AddDonation()
        {
            return View();
        }


        /// <summary>
        /// Get the Contact details and display in this admin view page.
        /// </summary>
        /// <returns></returns>

        public ActionResult GetContactDetails()
        {
            ContactRepositary contactRepository = new ContactRepositary();
            ModelState.Clear();
            return View(contactRepository.GetContactDetails());
        }



        /// <summary>
        /// Get the particular details to display in delete view page
        /// </summary>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns></returns>

        public ActionResult DeleteContactDetails(int id, ContactModel obj)
        {
            ContactRepositary contactRepository = new ContactRepositary();


            return View(contactRepository.GetContactDetails().Find(ContactModel => ContactModel.Id == id));


        }




        /// <summary>
        /// Delete the particular contact details and display it.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult DeleteContactDetails(int id)
        {
            try
            {
                ContactRepositary contactRepository = new ContactRepositary();

                if (contactRepository.DeleteContactDetails(id))
                {
                    ViewBag.AlertMessage("User Details Deleted Successfully");
                }
                return RedirectToAction("GetDetails");
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message);
                return View();
            }

        }



        /// <summary>
        /// call the function for get the details from the database with edit and delete
        /// </summary>
        /// <returns></returns>

        public ActionResult GetDetails()
        {
            RegistrationRepositary registerInfo = new RegistrationRepositary();
            ModelState.Clear();
            return View(registerInfo.GetDetails());
        }

        /// <summary>
        /// call the function for edit the user details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult EditDetails(int id)
        {
            RegistrationRepositary registerRepo = new RegistrationRepositary();
            return View(registerRepo.GetDetails().Find(Registration => Registration.id == id));
        }

        [HttpPost]
        public ActionResult EditDetails(int id, Registration registration)
        {
            try
            {
                RegistrationRepositary regRepo = new RegistrationRepositary();
                regRepo.EditDetails(registration);
                return RedirectToAction("GetDetails");
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message);
                return View();
            }
        }

        /// <summary>
        /// call the function for delete the particular record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns></returns>

        public ActionResult o(int id, Registration obj)
        {
            RegistrationRepositary RegisterRepo = new RegistrationRepositary();


            return View(RegisterRepo.GetDetails().Find(Registration => Registration.id == id));


        }


        [HttpPost]
        public ActionResult DeleteDetails(int id)
        {
            try
            {
                RegistrationRepositary RegisterRepo = new RegistrationRepositary();

                if (RegisterRepo.DeleteDetails(id))
                {
                    ViewBag.AlertMessage("User Details Deleted Successfully");
                }
                return RedirectToAction("GetDetails");
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message);
                return View();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public ActionResult Add()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Add( UserImageModel model)
        {

            string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);

            string extension = Path.GetExtension(model.ImageFile.FileName);

            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

            model.userImage = "/Content/Image/" + fileName;

            fileName = Path.Combine(Server.MapPath("~/Content/Image/"), fileName);

            model.ImageFile.SaveAs(fileName);

            UserImageModel modelImage = new UserImageModel();

            modelImage.Add(model);

            return View(model);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public ActionResult Show()
        {
            UserImageModel model = new UserImageModel();
            return View(model.Show());
        }


        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Home");
        }

        /// <summary>
        /// admin donation page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddAdminDonation()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddAdminDonation(AdminDonation uc, HttpPostedFileBase file)
        {


            string mainconn = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "insert into admin_donation (purpose,description,amount,Image) values(@purpose,@description,@amount,@Image) ";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlconn.Open();
            sqlcomm.Parameters.AddWithValue("@purpose", uc.purpose);
            sqlcomm.Parameters.AddWithValue("@description", uc.description);
            sqlcomm.Parameters.AddWithValue("@amount", uc.amount);
            //sqlcomm.Parameters.AddWithValue("@Image", uc.Gender);

            if (file != null && file.ContentLength > 0)
            {

                string filename = Path.GetFileName(file.FileName);
                string imgpath = Path.Combine(Server.MapPath("~/User-Images/"), filename);
                file.SaveAs(imgpath);

            }

            sqlcomm.Parameters.AddWithValue("@Image", "~/User-Images/" + file.FileName);
            sqlcomm.ExecuteNonQuery();
            sqlconn.Close();

            ViewData["Message"] = " Image Saved Successfully  !";


            return View();
        }





        /* string mainconn = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
          SqlConnection sqlconn = new SqlConnection(mainconn);
          string sqlquery = "insert into admin_donation(id,purpose,description,amount,Image) values(@id,@purpose,@description,@amount,@Image) ";
          SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
          sqlconn.Open();
          sqlcomm.Parameters.AddWithValue("@id", uc.id);
          sqlcomm.Parameters.AddWithValue("@purpose", uc.purpose);
          sqlcomm.Parameters.AddWithValue("@description", uc.description);
          sqlcomm.Parameters.AddWithValue("@amount", uc.amount);
         // sqlcomm.Parameters.AddWithValue("@Image", uc.Image);


          if (file != null && file.ContentLength > 0)
          {

              string filename = Path.GetFileName(file.FileName);
              string imgpath = Path.Combine(Server.MapPath("~/donationimages/"), filename);
              file.SaveAs(imgpath);

          }

          sqlcomm.Parameters.AddWithValue("@Image", "~/donationimages/" + file.FileName);
          sqlcomm.ExecuteNonQuery();
          sqlconn.Close();
                    return View();*/


        //  ViewData["Message"] = "User Record" + uc. + "Is Saved Successfully  !";



        /*   string fileName = Path.GetFileNameWithoutExtension(model.Imageinsert.FileName);

           string extension = Path.GetExtension(model.Imageinsert.FileName);

           fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

           model.Image = "/Content/Image/" + fileName;

           fileName = Path.Combine(Server.MapPath("~/Content/Image/"), fileName);

           model.Imageinsert.SaveAs(fileName);

           AdminDonation modelImage = new AdminDonation();

           modelImage.Add(model);

           return View(model); */



        // GET: AddAdminDonation
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
        public ActionResult EditAdminDonation(int id)
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
            //    admindonation.Image = dataTable.Rows[0][4].ToString();



                return View(admindonation);
            }
            return RedirectToAction("IndexAddAdminDonation");

        }

        // POST: IndexAddDetialsDonation/Edit/5
        [HttpPost]
        public ActionResult EditAdminDonation(int id, AdminDonation admindonation)
        {

            try
            {
                // TODO: Add update logic here

                using (SqlConnection con = new SqlConnection(Str))
                {
                    con.Open();
                    string q = " Update admin_donation  SET purpose=@purpose,description=@description,amount=@amount where id=@id ";
                    SqlCommand cmd = new SqlCommand(q, con);

                    cmd.Parameters.AddWithValue("@id", admindonation.id);
                    cmd.Parameters.AddWithValue("@purpose", admindonation.purpose);
                    cmd.Parameters.AddWithValue("@description", admindonation.description);
                    cmd.Parameters.AddWithValue("@amount", admindonation.amount);

                    cmd.ExecuteNonQuery();


                }

                return RedirectToAction("IndexAddAdminDonation");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(Str))
            {
                con.Open();
                string q = "Delete  from admin_donation where id=@id";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("IndexAddAdminDonation");
        }



    }
}

