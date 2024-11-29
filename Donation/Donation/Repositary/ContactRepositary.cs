using Donation.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Donation.Repositary
{
    public class ContactRepositary
    {



        private SqlConnection getconnection;

        /// <summary>
        /// Get the database connection
        /// </summary>

        private void connection()
        {
            string connection = ConfigurationManager.ConnectionStrings["Connection"].ToString();
            getconnection = new SqlConnection(connection);
        }

        /// <summary>
        /// Add the contact details into the database
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>~/Admin/Add</returns>

        public bool AddContact(ContactModel contact)
        {

            connection();
            SqlCommand command = new SqlCommand("sp_contact", getconnection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@type", "insert");

            command.Parameters.AddWithValue("@name", contact.Name);
            command.Parameters.AddWithValue("@email", contact.Email);
            command.Parameters.AddWithValue("@message", contact.Message);


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
        /// Get the contact details which is entered by user
        /// </summary>
        /// <returns></returns>

        public List<ContactModel> GetContactDetails()
        {
            connection();
            List<ContactModel> RegistrationList = new List<ContactModel>();

            SqlCommand command = new SqlCommand("sp_contact", getconnection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.AddWithValue("@type", "select");

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable datatable = new DataTable();
            getconnection.Open();
            dataAdapter.Fill(datatable);
            getconnection.Close();

            foreach (DataRow dr in datatable.Rows)
                RegistrationList.Add(
                    new ContactModel
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        Email = Convert.ToString(dr["Email"]),
                        Message = Convert.ToString(dr["Message"])

                    }
                    );
            return RegistrationList;
        }

        /// <summary>
        /// Delete the details which is entered by user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public bool DeleteContactDetails(int id)
        {
            connection();
            SqlCommand command = new SqlCommand("sp_contact", getconnection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@type", "delete");
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