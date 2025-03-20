using Backend_UtshobKotha.Models.Accounts;
using Microsoft.EntityFrameworkCore;

namespace Backend_UtshobKotha.Data
{
    public class UtshobKothaDbContext(DbContextOptions<UtshobKothaDbContext> options) : DbContext(options)
    {


        public DbSet<SignUp> NewUserRegistration => Set<SignUp>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SignUp>().HasData(
                 new SignUp
                 {
                     UserID = 1,
                     Email = "test@gmail.com",
                     Name = "Shamim",
                     Password = "test",
                     Role = "ADMIN"

                 }
            );
        }

    }
}
