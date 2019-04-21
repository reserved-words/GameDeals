using GameDeals.Core.Interfaces;
using GameDeals.Core.Model;
using System.Collections.Generic;
using System.Web.Http;

namespace GameDeals.API
{
    public class FeedsController : ApiController
    {
        private readonly IRssService _service;

        public FeedsController(IRssService service)
        {
            _service = service;
        }

        public IEnumerable<Feed> Get()
        {
            return _service.GetFeeds();
        }

        public Feed Get(int id)
        {
            return _service.GetFeed(id);
        }

        public void Post([FromBody]Feed value)
        {
            _service.AddFeed(value);
        }

        public void Put(int id, [FromBody]Feed value)
        {
            _service.UpdateFeed(value);
        }

        public void Delete(int id)
        {
            _service.DeleteFeed(id);
        }
    }
}
