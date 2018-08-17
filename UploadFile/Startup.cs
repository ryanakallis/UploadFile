using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UploadFile.Startup))]
namespace UploadFile
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
