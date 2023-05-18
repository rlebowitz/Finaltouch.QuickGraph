using Microsoft.AspNetCore.Components.QuickGrid;

namespace Finaltouch.QuickGraph.Web.Shared
{
    public class GridMetaData
    {
        public int StartIndex { get; set; }
        public int? Count { get; set; }
        public ICollection<SortedProperty>? SortProperties { get; set; }
        public Filter? Filter { get; set; }
    }
}
