using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ProjectFlow.Startup))]
namespace ProjectFlow
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }



    }
}
