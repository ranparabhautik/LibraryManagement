using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Model.Identity
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<IdentityRole<Guid>> roleManager)
        {
            string[] roles =
            {
                AppRoles.Admin,AppRoles.Member,AppRoles.Librarian
            };

            foreach(var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid> {Name = role});
                }
            }

            
        }
    }
}
