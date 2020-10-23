using System;
using System.ComponentModel.DataAnnotations;

namespace SPORTEA.SERVICES.EntityFramework
{
    public class UserEntity
    {
        [Key]
        public Int64 UserId { get; set; }

        public string SocialNetworkId { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Avatar { get; set; }

        public DateTime BirthDay { get; set; }

        public Int16 Gender { get; set; }

        public string AccountType { get; set; }
    }
}
