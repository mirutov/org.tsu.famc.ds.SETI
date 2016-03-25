using System;
using Owin;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Owin.Hosting;

namespace org.tsu.famc.ds.SETI
{
    public class Array
    {
        public int[] data;
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RESTReciverController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Receive(Array arg)
        {
            Console.Write("RESTReceiver.Receive: ");
            foreach (int i in arg.data)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            Task.Run(() =>
            {
                ISETI seti = SETIProxy.GetSETI();
                seti.AnalyzeData(arg.data);
            });
            return Ok();
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.EnableCors();
            config.Routes.MapHttpRoute(
              "Api", "api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional }
            );
            app.UseWebApi(config);
        }
    }

    public class RESTReciver
    {
        static public void RunRESTReciverControler(string url)
        {
            WebApp.Start<Startup>(url);
        }
    }
}
