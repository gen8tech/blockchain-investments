using Microsoft.AspNetCore.Authorization;
using Gen8.Ledger.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Gen8.Ledger.Api.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AboutController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationVersion;
        }

        [HttpGet]
        [Route("echo")]
        public string Echo(string message)
        {
            return message;
        }

        [HttpGet]
        [Route("getid")]
        public string GetId()
        {
            return Util.NewSequentialId().ToString();
        }
    }
}
