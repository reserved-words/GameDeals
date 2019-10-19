using GameDeals.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GameDeals.API
{
    [Authorize]
    [Route("categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IRssService _service;

        public CategoriesController(IRssService service, ILogger logger)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Get()
        {
            try
            {
                return Ok(_service.GetCategories());
            }
            catch (System.Exception ex)
            {
                _logger.Log(ex);
                throw ex;
            }
        }
    }
}
