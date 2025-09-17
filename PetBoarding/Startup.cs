using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PetBoarding.Startup))]
namespace PetBoarding
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
