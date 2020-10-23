using System;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SPORTEA.SERVICES.EntityFramework
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<UserEntity> UserEntities { get; set; } 

        public DatabaseContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=103.130.215.156;Database=Sportea;Integrated Security=False;User ID=sa;Password=noisaoduoc;");
        }
    }
}
