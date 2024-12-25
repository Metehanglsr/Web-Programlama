using BarberApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
namespace BarberApp.Data
{


    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, BarberDbContext context, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            // Rolleri kontrol ediyoruz
            string[] roleNames = new[] { "Admin", "Customer" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(roleName));
                }
            }

            // Admin kullanıcısını ekliyoruz
            var adminUser = await userManager.FindByEmailAsync("admin@example.com");
            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com"
                };
                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Customer kullanıcısını ekliyoruz
            var customerUser = await userManager.FindByEmailAsync("customer@example.com");
            if (customerUser == null)
            {
                customerUser = new User
                {
                    UserName = "customer@example.com",
                    Email = "customer@example.com"
                };
                var result = await userManager.CreateAsync(customerUser, "Customer123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(customerUser, "Customer");
                }
            }
        }
    }

}
