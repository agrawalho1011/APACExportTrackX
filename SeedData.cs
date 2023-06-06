using APACExportTrackX.DataModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace APACExportTrackX
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDBContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDBContext>>()))
            {
                if (context.Roles.Count() == 0)
                {
                    string[] roles = { "Supervisor", "Manager", "User", "QC" };
                    foreach (string role in roles)
                    {
                        context.Roles.Add(new IdentityRole { Name = role });
                    }
                    context.SaveChanges();
                }

                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var user = new ApplicationUser
                {
                    UserName = "TusharP",
                    EmpId = 139289,
                    CitrixId = "tusharp",
                    FirstName = "Tushar",
                    LastName = "Parik",
                };

                var result = userManager.CreateAsync(user, "Abcd@1234").Result;
                if (result.Succeeded)
                {
                    var userResult = userManager.AddToRoleAsync(user, "Supervisor");
                }

                //context.SaveChanges();
            }
        }
    }

}


