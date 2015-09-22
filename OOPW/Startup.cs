using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OOPW.Startup))]
namespace OOPW
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
