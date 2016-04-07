using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ContosoUDemo.Startup))]
namespace ContosoUDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
