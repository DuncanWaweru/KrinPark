using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KrinPark.Startup))]
namespace KrinPark
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
