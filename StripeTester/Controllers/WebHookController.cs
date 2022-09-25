using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe;
using StripeTester.Services.Interfaces;

namespace StripeTester.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebHookController : Controller
    {
        IStripeEventsService Service;
        public WebHookController(IStripeEventsService service)
        {
            Service = service;
        }       

        [HttpPost("WebhookListener")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var resp = Service.AddData(json);
                if (resp)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (StripeException e)
            {
                return BadRequest(e);
            }
        }
    }
}
