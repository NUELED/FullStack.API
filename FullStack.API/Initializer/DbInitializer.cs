using FullStack.API.Data;
using FullStack.API.Helper;
using FullStack.API.Initializer.Interface;
using FullStack.API.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly FullstackDbContext _db;

        public DbInitializer(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,
            FullstackDbContext db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }

        public void Initialize()
        {
            try
            {
               
                // Apply any pending migrations
                 //    _db.Database.Migrate();  I commented this out bcos of the error it was throwing

                // Check if AspNetRoles table already exists
                bool rolesExist = _db.Roles.Any(r => r.Name == SD.Role_Admin || r.Name == SD.Role_Customer);
                if (!rolesExist)
                {
                    // Create roles if they don't exist
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                }


                // Check if user already exists
                if (!_userManager.Users.Any(u => u.UserName == "nuel@briz.com"))
                {
                    // Create a new user
                    AppUser user = new()
                    {
                        UserName = "nuel@briz.com",
                        Email = "nuel@briz.com",
                        EmailConfirmed = true,
                    };

                    // Add user to Customer role
                    IdentityResult result = _userManager.CreateAsync(user, "Password1@").GetAwaiter().GetResult();
                    if (result.Succeeded)
                    {
                        _userManager.AddToRoleAsync(user, SD.Role_Customer).GetAwaiter().GetResult();
                    }
                    else
                    {
                        throw new Exception($"Failed to create user: {string.Join(", ", result.Errors)}");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
