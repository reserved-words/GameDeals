using GameDeals.Core.Interfaces;
using GameDeals.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GameDeals.API
{
    [Authorize]
    [Route("feeds")]
    public class FeedsController : ControllerBase
    {
        private readonly IRssService _service;

        public FeedsController(IRssService service)
        {
            _service = service;
        }

        public IActionResult Get()
        {
            return Ok(_service.GetFeeds());
        }

        //public Feed Get(int id)
        //{
        //    return _service.GetFeed(id);
        //}

        //public void Post(Feed value)
        //{
        //    _service.AddFeed(value);
        //}

        //public void Put(int id, Feed value)
        //{
        //    _service.UpdateFeed(value);
        //}

        //public void Delete(int id)
        //{
        //    _service.DeleteFeed(id);
        //}
    }
}
