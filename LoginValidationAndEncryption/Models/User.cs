using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginValidationAndEncryption.Models
{
    public class User
    {   
        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }

        public virtual string Password { get; set; }

    }
}