using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Donation
{
    public class Connection
    {

        static SqlConnection src;

        public static SqlConnection getConnection()
        {
            if (src == null)
            {
                src = new SqlConnection();
                src.ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
                src.Open();
            }
            return src;
        }




    }
}