using CatholicSee.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatholicSee.Data.Auth
{
    public class RegisterRequest
    {
        public User User { get; set; }
        public string Password { get; set; }
    }
}
