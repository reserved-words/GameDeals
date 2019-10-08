using GameDeals.Core.Interfaces;
using GameDeals.Core.Model;
using System.Web.Http;

namespace GameDeals.API
{
    public class PostsController : ApiController
    {
        private readonly IRssService _service;
        
        public PostsController(IRssService service)
        {
            _service = service;
        }

        public PagedResult<Post> Get(int id, int limit, int offset)
        {
            return _service.GetPosts(id, limit, offset);
        }
    }
}
