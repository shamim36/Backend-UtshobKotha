using Backend_UtshobKotha.Models;
using Backend_UtshobKotha.Models.Accounts;
using Microsoft.EntityFrameworkCore;

namespace Backend_UtshobKotha.Data
{
    public class UtshobKothaDbContext(DbContextOptions<UtshobKothaDbContext> options) : DbContext(options)
    {


        public DbSet<SignUp> NewUserRegistration => Set<SignUp>();
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SignUp>().HasData(
                 new SignUp
                 {
                     UserID = "213-35-775",
                     Email = "test@gmail.com",
                     Name = "Shamim",
                     Password = "test",
                     Role = "ADMIN"

                 }
            );
            // Configure the Event entity
            modelBuilder.Entity<Event>()
                .Property(e => e.Category)
                .HasConversion<string>(); // Store enum as string in the database

            // Ensure Title is required and has a max length
            modelBuilder.Entity<Event>()
                .Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);

            // Ensure Description is required and has a max length
            modelBuilder.Entity<Event>()
                .Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(1000);

            // Ensure Location is required and has a max length
            modelBuilder.Entity<Event>()
                .Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(200);

            // Ensure Capacity is required and has a minimum value
            modelBuilder.Entity<Event>()
                .Property(e => e.Capacity)
                .IsRequired();

            // Ensure IsFree is required
            modelBuilder.Entity<Event>()
                .Property(e => e.IsFree)
                .IsRequired();
        }

    }
}
