using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PL_MVC.Startup))]
namespace PL_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
