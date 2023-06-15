namespace Finaltouch.QuickGrid.Web.Shared
{
    public class Filter
    {
        public string? Field { get; set; }
        public string? Value { get; set; }
        public Operator Operator { get; set; } = Operator.Equals;
        public bool IsValid => Field != null && Value != null;

    }
}
