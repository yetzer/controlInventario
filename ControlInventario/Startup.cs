using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ControlInventario.Startup))]
namespace ControlInventario
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
