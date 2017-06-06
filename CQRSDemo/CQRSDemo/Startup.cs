using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CQRSDemo.Startup))]
namespace CQRSDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
