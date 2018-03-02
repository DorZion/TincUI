using System.Web.Http;

namespace Tinc.Communication.Management.WebAPI.Controllers
{
    public class HelloWorldController : ApiController
    {
        public string Get()
        {
            return "Hello, World!";
        }
    }
}