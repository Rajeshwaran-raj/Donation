using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Donation.Myfolder
{
    public class Password
    {


        public string Encode(string password)
        {
            try
            {
                byte[] EnDataByte = new byte[password.Length];
                EnDataByte = System.Text.Encoding.UTF8.GetBytes(password);
                string EncryptedData = Convert.ToBase64String(EnDataByte);
                return EncryptedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in Encode" + e.Message);
            }
        }
    }
}