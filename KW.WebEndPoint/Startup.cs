using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KW.Presentation.WebEndPoint.Startup))]
namespace KW.Presentation.WebEndPoint
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
