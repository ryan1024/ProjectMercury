using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mercury.Web.Tractivo.Startup))]
namespace Mercury.Web.Tractivo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
