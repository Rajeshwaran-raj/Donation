using DocumentFormat.OpenXml.Wordprocessing;
using Donation.Myfolder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Donation.Models
{
    public class AdminDonation
    {

        [Display(Name = "Id")]

        public int id { get; set; }

        [Display(Name = "Purpose")]
        public string purpose { get; set; }

        [Display(Name = "Description")]

        public string description { get; set; }

        [Display(Name = "Amount")]

        public string amount { get; set; }

        [Display(Name = "Upload image")]

        public string Image { get; set; }

        public HttpPostedFileBase Imageinsert { get; set; }

        public void Add(AdminDonation model)
        {
            try
            {
                SqlCommand command = new SqlCommand("insert into admin_donation values('" + model.id + "', '"+model.purpose+ "' ,'" + model.description + "','" + model.amount + "','" + model.Image + "')", Connection.getConnection());
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message);
            }
        }

        public List<AdminDonation> Show()

        {
            SqlCommand command = new SqlCommand("select * from admin_donation", Connection.getConnection());
            List<AdminDonation> list = new List<AdminDonation>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new AdminDonation { id = Convert.ToInt32(reader["id"]), purpose = reader["purpose"].ToString(), description = reader["description"].ToString(), amount = reader["amount"].ToString() ,Image = reader["Image"].ToString() });



            }
            reader.Close();
            return list;

        }






    }
}