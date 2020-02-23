using GameDeals.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.GetFeeds());
        }
    }
}
