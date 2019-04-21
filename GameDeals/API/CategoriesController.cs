using System.Collections.Generic;
using System.Web.Http;
using GameDeals.Core.Interfaces;
using GameDeals.Core.Model;

namespace GameDeals.API
{
    public class CategoriesController : ApiController
    {
        private readonly IRssService _service;

        public CategoriesController(IRssService service)
        {
            _service = service;
        }

        public IEnumerable<Category> Get()
        {
            return _service.GetCategories();
        }
    }
}
