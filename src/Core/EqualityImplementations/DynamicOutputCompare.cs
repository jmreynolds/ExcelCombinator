using System.Collections.Generic;
using System.Linq;
using Core.Extensions;
using Core.Models;

namespace Core.EqualityImplementations
{
    public class DynamicOutputCompare : IEqualityComparer<DynamicOutput>
    {
        public bool Equals(DynamicOutput x, DynamicOutput y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;

            var xDupes = x.DynamicItems.Where(xItem => xItem.ShouldRemoveDupes && xItem.Value != null).ToList();
            var yDuped = y.DynamicItems.Where(yItem => yItem.ShouldRemoveDupes && yItem.Value != null).ToList();
            bool result = true;
            xDupes.ForEach(xItem =>
                           {
                               string s = xItem.Value.ToString();
                               if (yDuped.GetStringValueFromFieldInList(xItem.ColumnName).RemoveExtraSpaces() != s.RemoveExtraSpaces()) result = false;
                           });
            return result;
        }


        
        

        public int GetHashCode(DynamicOutput obj)
        {
            var list = obj.DynamicItems
                .Where(x => x.ShouldRemoveDupes)
                .Select(x => x.Value)
                .OfType<string>()
                .ToArray();
            int joined = string.Join("", list).ToUpper().GetHashCode();
            return joined;
        }
    }
}