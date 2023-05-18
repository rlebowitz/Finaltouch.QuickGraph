using Finaltouch.QuickGraph.Web.Shared;
using Finaltouch.QuickGraph.Web.Shared.Models;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Finaltouch.QuickGraph.Web.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [Route("[controller]")]
    public class BabyNamesController : ControllerBase
    {
        private ILogger<BabyNamesController> Logger { get; set; }
        private BabynamesContext Context { get; set; }

        public BabyNamesController(ILogger<BabyNamesController> logger, BabynamesContext context)
        {
            Logger = logger;
            Context = context;
        }

        [HttpPost]
        public NamesResult? GetBabyNames([FromBody] GridMetaData metaData)
        {
            try
            {
                var ordering = Ordering(metaData.SortProperties);
                var ordered = string.IsNullOrEmpty(ordering)
                    ? Context.Babynames.OrderBy(t => t.State)
                    : Context.Babynames.OrderBy(ordering);
                var result = ordered
                    .Filter(metaData.Filter)
                    .Select(t => t)
                    .Skip(metaData.StartIndex)
                    .Take(metaData.Count ?? 10)
                    .AsNoTracking()
                    .ToListAsync();
                var count = Context.Babynames.AsQueryable().Filter(metaData.Filter).CountAsync();

                Task.WaitAll(count, result);

                return new NamesResult
                {
                    Count = count.Result,
                    BabyNames = result.Result
                };
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Controller Error");
            }
            return default;
        }

        //https://dynamic-linq.net/basic-simple-query#ordering-results
        protected static string Ordering(ICollection<SortedProperty>? properties)
        {
            List<string> columns = new();
            if (properties == null)
            {
                return string.Empty;
            }
            foreach (var property in properties)
            {
                if (property.Direction == SortDirection.Ascending)
                {
                    columns.Add(property.PropertyName);
                }
                else
                {
                    columns.Add($"{property.PropertyName} desc");
                }
            }
            return string.Join(", ", columns);
        }
    }
}