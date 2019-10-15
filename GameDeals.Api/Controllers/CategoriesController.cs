using System.Web.Http;
using GameDeals.Core.Interfaces;

namespace GameDeals.API
{
    public class CategoriesController : ApiController
    {
        private readonly ILogger _logger;
        private readonly IRssService _service;

        public CategoriesController(IRssService service, ILogger logger)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_service.GetCategories());
            }
            catch (System.Exception ex)
            {
                _logger.Log(ex);
                return InternalServerError(ex);
            }
        }
    }
}
