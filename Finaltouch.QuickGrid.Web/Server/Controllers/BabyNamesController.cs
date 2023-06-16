using Finaltouch.QuickGrid.Web.Server.Services;
using Finaltouch.QuickGrid.Web.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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

        [HttpGet]
        public NamesResult? GetBabyNames([FromQuery] string metaData)
        {
            var data = JsonSerializer.Deserialize<GridMetaData>(Base64Url.Decode(metaData));
            return data != null ? NamesRepository.GetBabyNames(data) : null;
        }

    }
}