using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Wrzutka.Startup))]
namespace Wrzutka
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
