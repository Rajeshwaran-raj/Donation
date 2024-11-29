using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using Donation.Models;
using Donation.Myfolder;

namespace Donation.Repositary
{
    public class RegistrationRepositary
    {
        private SqlConnection getconnection;

        /// <summary>
        /// Get the connection from database
        /// </summary>

        private void connection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
            getconnection = new SqlConnection(connectionString);
        }


        /// <summary>
        /// Add the user details in signup form to the database
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        /// 

        public bool AddDetails(Registration registration)
        {
            Password EncryptData = new Password();

            connection();
            SqlCommand command = new SqlCommand("sp_main", getconnection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@firstname", registration.firstname);
            command.Parameters.AddWithValue("@lastname", registration.lastname);
            command.Parameters.AddWithValue("@dateofbirth", registration.dateofbirth);
            command.Parameters.AddWithValue("@age", registration.age);
            command.Parameters.AddWithValue("@Email", registration.Email);
            command.Parameters.AddWithValue("@phone", registration.phone);
            command.Parameters.AddWithValue("@address", registration.address);
            command.Parameters.AddWithValue("@pincode", registration.pincode);
            command.Parameters.AddWithValue("@password", EncryptData.Encode(registration.password));
            command.Parameters.AddWithValue("@confirmpassword", EncryptData.Encode(registration.confirmpassword));
            command.Parameters.AddWithValue("@type", "sp_insert");

            getconnection.Open();
            int i = command.ExecuteNonQuery();
            getconnection.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    

        /// <summary>
        /// Get the signup user details from database 
        /// </summary>
        /// <returns></returns>

        public List<Registration> GetDetails()
        {
            connection();
            List<Registration> RegistrationList = new List<Registration>();

            SqlCommand command = new SqlCommand("sp_main", getconnection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.AddWithValue("@type", "sp_select");

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable datatable = new DataTable();
            getconnection.Open();
            dataAdapter.Fill(datatable);
            getconnection.Close();

            foreach (DataRow dr in datatable.Rows)
                RegistrationList.Add(
                    new Registration
                    {
                        id = Convert.ToInt32(dr["id"]),
                        firstname = Convert.ToString(dr["firstName"]),
                        lastname = Convert.ToString(dr["lastName"]),
                        dateofbirth = Convert.ToString(dr["dateofbirth"]),
                        age = Convert.ToString(dr["age"]),
                        Email = Convert.ToString(dr["Email"]),
                        phone = Convert.ToString(dr["phone"]),
                        address = Convert.ToString(dr["address"]),
                        pincode = Convert.ToString(dr["pincode"]),
                        //Password = Convert.ToString(dr["Password"]),
                        //ConfirmPassword = Convert.ToString(dr["ConfirmPassword"])
                    }
                    );
            return RegistrationList;
        }

        /// <summary>
        /// Edit(Update) the user signup details
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>

        public bool EditDetails(Registration registration)
        {
            connection();
            SqlCommand command = new SqlCommand("sp_main", getconnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", registration.id);
            command.Parameters.AddWithValue("@firstname", registration.firstname);
            command.Parameters.AddWithValue("@lastname", registration.lastname);
            command.Parameters.AddWithValue("@dateofbirth", registration.dateofbirth);
            command.Parameters.AddWithValue("@age", registration.age);
            command.Parameters.AddWithValue("@Email", registration.Email);
            command.Parameters.AddWithValue("@phone", registration.phone);
            command.Parameters.AddWithValue("@address", registration.address);
            command.Parameters.AddWithValue("@pincode", registration.pincode);
            command.Parameters.AddWithValue("@password", registration.password);
            command.Parameters.AddWithValue("@confirmpassword", registration.confirmpassword);
            command.Parameters.AddWithValue("@type", "sp_update");

            getconnection.Open();
            int i = command.ExecuteNonQuery();
            getconnection.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Delete the user details which is selected.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public bool DeleteDetails(int id)
        {
            connection();
            SqlCommand command = new SqlCommand("sp_main", getconnection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@type", "sp_delete");
            getconnection.Open();
            int i = command.ExecuteNonQuery();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}