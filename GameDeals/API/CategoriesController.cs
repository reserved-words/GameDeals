using System.Collections.Generic;
using System.Web.Http;
using GameDeals.Core.Interfaces;
using GameDeals.Core.Model;

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

        public IEnumerable<Category> Get()
        {
            try
            {
                return _service.GetCategories();
            }
            catch (System.Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
        }
    }
}
