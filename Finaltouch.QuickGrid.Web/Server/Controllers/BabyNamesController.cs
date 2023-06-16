using Finaltouch.QuickGrid.Web.Server.Services;
using Finaltouch.QuickGrid.Web.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Finaltouch.QuickGrid.Web.Server.Controllers
{
    [Route("api/[controller]/[action]")]
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