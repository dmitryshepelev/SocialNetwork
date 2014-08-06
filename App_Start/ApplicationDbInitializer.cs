using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SocialNetwork.Models;

namespace SocialNetwork
{
    public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            roleManager.Create(new IdentityRole { Name = "admin" });
            roleManager.Create(new IdentityRole { Name = "user" });

            var admin = new ApplicationUser { UserName = "admin", Email = "admin@admin.com" };
            var result = userManager.Create(admin, "admin1");
            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, "admin");
            }
            base.Seed(context);
        }
    }
}