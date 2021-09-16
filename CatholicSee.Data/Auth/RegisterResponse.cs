using System;
using System.Collections.Generic;
using System.Text;

namespace CatholicSee.Data.Auth
{
    public class RegisterResponse
    {
        public bool Succeeded { get; set; }
        public IEnumerable<RegisterError> Errors { get; set; }
    }
}
