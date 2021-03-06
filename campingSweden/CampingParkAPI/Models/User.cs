﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CampingParkAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        [NotMapped]
        public string Token { get; set; }




        public string PasswordCrypt(string password)
        {
            string hiddenPassword = "";
            for (int i = 0; i < password.Length; i++)
            {
                hiddenPassword += "*";
            }
            return hiddenPassword;
        }

    }
}
