using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Crawler = PCRArena.PCRArenaCrawler;

namespace PCRArenaCrawler.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PCRArenaController : ControllerBase
    {

        private Crawler crawler;

        public PCRArenaController()
        {
            crawler = new Crawler();
        }

        [HttpGet]
        public string Get()
        {
            return "Test";
        }
    }
}