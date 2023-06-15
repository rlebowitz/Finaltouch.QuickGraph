using System.Linq.Dynamic.Core;

namespace Finaltouch.QuickGrid.Web.Shared
{
    public static class QueryableExtensions
    {

        public static IQueryable<TItem> Filter<TItem>(this IQueryable<TItem> queryable, Filter? filter)
        {
            if (filter == null || !filter.IsValid)
            {
                return queryable;
            }
            var predicate = filter.Operator switch
            {
                Operator.Equals => $"{filter.Field} == @0",
                Operator.NotEquals => $"{filter.Field} != @0",
                Operator.GreaterThan => $"{filter.Field} > @0",
                Operator.LessThan => $"{filter.Field} < @0",
                Operator.Contains => $"{filter.Field}.ToLower().Contains(@0.ToLower())",
                _ => throw new NotImplementedException(),
            };
            return queryable.Where(predicate, filter.Value);
        }
        public static IQueryable<TItem> Filter<TItem>(this IOrderedQueryable<TItem> queryable, Filter? filter)
        {
            if (filter == null || !filter.IsValid)
            {
                return queryable;
            }
            var predicate = filter.Operator switch
            {
                Operator.Equals => $"{filter.Field} == @0",
                Operator.NotEquals => $"{filter.Field} != @0",
                Operator.GreaterThan => $"{filter.Field} > @0",
                Operator.LessThan => $"{filter.Field} < @0",
                Operator.Contains => $"{filter.Field}.ToLower().Contains(@0.ToLower())",
                _ => throw new NotImplementedException(),
            };
            return queryable.Where(predicate, filter.Value);
        }
    }
}
