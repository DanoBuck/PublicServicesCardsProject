using Microsoft.Owin;
using Owin;

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
