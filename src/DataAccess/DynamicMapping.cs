using System.Collections.Generic;
using System.Linq;
using Core.Models;

namespace DataAccess
{
    public static class DynamicMapping
    {
        public static List<DynamicItem> MapFields(this IEnumerable<DynamicItem> inputList)
        {
            var result = inputList.Where(x => x.ShouldIncludeInOutput && !x.ShouldAggregate).Select(dynamicItem => new DynamicItem()
            {
                ColumnName = dynamicItem.ColumnName,
                Value = dynamicItem.Value,
                ShouldRemoveDupes = dynamicItem.ShouldRemoveDupes,
                ShouldAggregate = dynamicItem.ShouldAggregate
            }).ToList();
            return result;
        }
    }
}