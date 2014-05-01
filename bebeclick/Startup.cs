using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(bebeclick.Startup))]
namespace bebeclick
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
