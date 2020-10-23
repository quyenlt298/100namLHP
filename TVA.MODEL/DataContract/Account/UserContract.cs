using System;
using System.Collections.Generic;
using IdentityModel;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace TVA.MODEL.Account
{
    public class UserContract
    {
        public UserProfile UserProfile { get; set; }
    }
}
