using Donation.Myfolder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Donation.Models
{
    public class UserImageModel
    {



        [Display(Name = "Id")]
        public int userId { get; set; }

        [Display(Name = "Upload image")]
        public string userImage { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        public void Add(UserImageModel model)
        {
            try
            {
                SqlCommand command = new SqlCommand("insert into userImage values('" + model.userId + "','" + model.userImage + "')", Connection.getConnection());
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message);
            }
        }

        public List<UserImageModel> Show()

        {
            SqlCommand command = new SqlCommand("select * from userImage", Connection.getConnection());
            List<UserImageModel> list = new List<UserImageModel>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new UserImageModel { userId = Convert.ToInt32(reader["userId"]), userImage = reader["userImage"].ToString() });



            }
            reader.Close();
            return list;





        }
    }
}