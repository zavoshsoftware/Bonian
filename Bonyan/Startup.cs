using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bonyan.Startup))]
namespace Bonyan
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
