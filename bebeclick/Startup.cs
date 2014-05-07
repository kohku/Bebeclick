using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bebeclick.Startup))]
namespace Bebeclick
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
