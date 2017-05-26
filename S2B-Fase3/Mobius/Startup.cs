using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mobius.Startup))]
namespace Mobius
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
