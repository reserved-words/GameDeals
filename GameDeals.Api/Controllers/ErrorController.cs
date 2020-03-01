using GameDeals.Core.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GameDeals.Api.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger _logger;

        public ErrorController(ILogger logger)
        {
            _logger = logger;
        }

        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Error() 
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            _logger.Log(context.Error);

            return Problem();
        }
    }
}