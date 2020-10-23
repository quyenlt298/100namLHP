using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TVA.MODEL.Account;

namespace SPORTEA.SERVICES.Infrastructure
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<UserProfile> UserProfiles { get; set; }

        public DatabaseContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=103.130.215.156;Database=100namLHP;Integrated Security=False;User ID=sa;Password=noichaduoc123;");
        }
    }
}
