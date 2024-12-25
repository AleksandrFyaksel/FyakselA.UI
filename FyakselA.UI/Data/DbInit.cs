using FyakselA.UI.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FyakselA.UI.Data
{
    public class DbInit
    {
        public static async Task SetupIdentityAdmin(WebApplication application)
        {
            using var scope = application.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

           
            var user = await userManager.FindByEmailAsync("admin@gmail.com");
            if (user == null)
            {
                user = new AppUser
                {
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com", 
                    EmailConfirmed = true 
                };

                
                var result = await userManager.CreateAsync(user, "123456");
                if (result.Succeeded)
                {
                    
                    var claim = new Claim(ClaimTypes.Role, "admin");
                    await userManager.AddClaimAsync(user, claim);
                }
                else
                {
                    
                    foreach (var error in result.Errors)
                    {
                        
                        Console.WriteLine($"Error: {error.Description}");
                    }
                }
            }
        }
    }
}