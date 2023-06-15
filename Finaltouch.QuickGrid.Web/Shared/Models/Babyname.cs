namespace Finaltouch.QuickGrid.Web.Shared.Models;

public partial class Babyname
{
    public string State { get; set; } = null!;

    public int Year { get; set; }

    public string Name { get; set; } = null!;

    public string Sex { get; set; } = null!;

    public int Count { get; set; }

    public int RankWithinSex { get; set; }

    public double Per100kWithinSex { get; set; }
}
