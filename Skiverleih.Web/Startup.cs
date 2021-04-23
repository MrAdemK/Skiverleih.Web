using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Skiverleih.Web.Startup))]
namespace Skiverleih.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
