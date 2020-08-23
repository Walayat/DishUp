using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DishUp.Startup))]
namespace DishUp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
