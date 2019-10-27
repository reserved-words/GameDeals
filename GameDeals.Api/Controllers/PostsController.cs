using GameDeals.Core.Interfaces;
using GameDeals.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameDeals.API
{
    [Authorize]
    [Route("posts")]
    public class PostsController : ControllerBase
    {
        private readonly IRssService _service;

        public PostsController(IRssService service)
        {
            _service = service;
        }

        [HttpGet("{id}/{limit}/{offset}")]
        public IActionResult Get(int id, int limit, int offset)
        {
            return Ok(_service.GetPosts(id, limit, offset));
        }
    }
}
