using System;
using Nancy;

using Owin;
using Nowin;
using Microsoft.Owin.Builder;
using System.Net;

namespace SelfHostWithOwin
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var app = new AppBuilder();
            var startup = new Startup();
            startup.Configuration(app);
            var appFunc = app.Build();

            using (var server = ServerBuilder.
                                New()
                                .SetEndPoint(new System.Net.IPEndPoint(IPAddress.Loopback, 1926))
                                .SetOwinApp(appFunc)
                                .Build())
            {
                Console.WriteLine("Running...");
                Console.ReadKey();
            }
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
        }
    }

    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => "Hi";
        }
    }
}
