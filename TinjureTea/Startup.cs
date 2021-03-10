using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TinjureTea.Startup))]
namespace TinjureTea
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
