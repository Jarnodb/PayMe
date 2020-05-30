using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PayMeDAL.Models;
using System;

namespace PayMeDAL.Context
{
    public static class ModelBuilderExtensions
    {
        private static PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
        public static void Seed(this ModelBuilder modelBuilder)
        {
            ApplicationUser jarno = new ApplicationUser();

            jarno.Id = Guid.NewGuid().ToString();
            jarno.Email = "a@a.com";
            jarno.UserName = "a@a.com";
            jarno.NormalizedEmail = "A@A.COM";
            jarno.NormalizedUserName = "A@A.COM";
            jarno.PasswordHash = passwordHasher.HashPassword(jarno, "Server2016!");
            jarno.UniqueCode = "AZERTY";
            

            modelBuilder.Entity<ApplicationUser>().HasData(jarno);
        }
    }
}
