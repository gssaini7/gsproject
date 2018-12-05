using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(crudgenerator.Startup))]
namespace crudgenerator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
