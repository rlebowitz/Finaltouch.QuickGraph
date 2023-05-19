using Finaltouch.QuickGraph.Web.Server.Services;
using Finaltouch.QuickGraph.Web.Shared;
using Finaltouch.QuickGraph.Web.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Finaltouch.QuickGraph.Web.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [Route("[controller]")]
    public class BabyNamesController : ControllerBase
    {
        private INamesRepository NamesRepository { get; set; }
        public BabyNamesController(INamesRepository namesRepository)
        {
            NamesRepository = namesRepository;
        }

        [HttpPost]
        public NamesResult? GetBabyNames([FromBody] GridMetaData metaData)
        {
            return NamesRepository.GetBabyNames(metaData);
        }

    }
}