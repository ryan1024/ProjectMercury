using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mercury.Backoffice.Startup))]
namespace Mercury.Backoffice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
