﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Donation.Models
{
    public class LoginMod
    {
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}