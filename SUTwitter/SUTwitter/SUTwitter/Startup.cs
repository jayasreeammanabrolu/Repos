using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SUTwitter.Startup))]
namespace SUTwitter
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
