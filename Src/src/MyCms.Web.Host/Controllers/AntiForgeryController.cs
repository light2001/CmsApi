using Microsoft.AspNetCore.Antiforgery;
using MyCms.Controllers;

namespace MyCms.Web.Host.Controllers
{
    public class AntiForgeryController : MyCmsControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
