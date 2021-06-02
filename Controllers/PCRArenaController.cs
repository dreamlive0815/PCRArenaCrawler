using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PCRArena;
using Crawler = PCRArena.PCRArenaCrawler;

using Core.Common;

using SimpleHTTPClient;

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

        private async Task<string> GetRawBody()
        {
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }

        [HttpPost]
        public async Task<APIResult<string>> AttackTeamQuery()
        {
            var encrpted = await GetRawBody();
            var raw = SimpleEncryptor.Default.Decrypt(encrpted);
            var args = JsonSerializer.Deserialize<ArenaAttackTeamQueryParams>(raw);
            var r = crawler.AttackTeamQueryRaw(args);
            return new APIResult<string>()
            {
                Code = 0,
                Message = "APIResult",
                Data = r,
            };
        }
    }
}