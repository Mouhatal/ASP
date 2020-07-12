using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebGL.Startup))]
namespace WebGL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
