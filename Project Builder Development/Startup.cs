using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project_Builder_Development.Startup))]
namespace Project_Builder_Development
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
