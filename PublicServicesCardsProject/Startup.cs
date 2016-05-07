using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using PublicServicesCardsProject.Models;

[assembly: OwinStartupAttribute(typeof(PublicServicesCardsProject.Startup))]
namespace PublicServicesCardsProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        public void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Manager"))
            {
                var role = new IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "DaniielBuckleyTy3@gmail.com";
                user.Email = user.UserName;

                string password = "P@ssWord1'";

                var checkUser = userManager.Create(user, password);

                if (checkUser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "Manager");
                }
            }

            if (!roleManager.RoleExists("Staff"))
            {
                var role = new IdentityRole();
                role.Name = "Staff";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "Staff@gmail.com";
                user.Email = user.UserName;

                string password = "P@ssWord1'";

                var checkUser = userManager.Create(user, password);

                if (checkUser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "Staff");
                }
            }

            if (!roleManager.RoleExists("Customer"))
            {
                var role = new IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "Customer@gmail.com";
                user.Email = user.UserName;

                string password = "P@ssWord1'";

                var checkUser = userManager.Create(user, password);

                if (checkUser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "Customer");
                }
            }
        }
    }
}
