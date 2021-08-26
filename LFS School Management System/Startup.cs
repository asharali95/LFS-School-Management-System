using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LFS_School_Management_System.Startup))]
namespace LFS_School_Management_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
