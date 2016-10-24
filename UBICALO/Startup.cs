using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UBICALO.Startup))]
namespace UBICALO
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
