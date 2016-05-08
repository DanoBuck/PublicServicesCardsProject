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
        }
    }
}
