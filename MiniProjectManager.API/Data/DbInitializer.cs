using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MiniProjectManager.API.Models.Entities;
using MiniProjectManager.API.Helpers;

namespace MiniProjectManager.API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Ensure database is created
            context.Database.Migrate();

            // Seed a test user if not exists
            if (!context.Users.Any())
            {
                var passwordHash = PasswordHelper.HashPassword("Admin@123");

                context.Users.Add(new User
                {
                    Username = "admin",
                    Email = "admin@example.com",
                    PasswordHash = passwordHash,
                    CreatedAt = DateTime.UtcNow
                });

                context.SaveChanges();
            }
        }
    }
}
