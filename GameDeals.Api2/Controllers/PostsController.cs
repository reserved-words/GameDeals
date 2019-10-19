//using GameDeals.Core.Interfaces;
//using GameDeals.Core.Model;
//using Microsoft.AspNetCore.Mvc;

//namespace GameDeals.API
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class PostsController : ControllerBase
//    {
//        private readonly IRssService _service;

//        public PostsController(IRssService service)
//        {
//            _service = service;
//        }

//        public PagedResult<Post> Get(int id, int limit, int offset)
//        {
//            return _service.GetPosts(id, limit, offset);
//        }
//    }
//}
