using System.Collections.Generic;
using System.Linq;
using Core.Models;

namespace Core.Extensions
{
    public static class DynamicBaseExtensions
    {
        public static string GetStringValueFromFieldInList(this List<DynamicItem> dynamicItems, string columnName)
        {
            return dynamicItems.Find(item => item.ColumnName == columnName)?.Value?.ToString()
                ?? string.Empty;
        }
        public static string GetStringValueFromColumn(this DynamicBase dynamicBase, string columnName)
        {
            return dynamicBase?.DynamicItems?.Find(item => item?.ColumnName == columnName)?.Value?.ToString()
                ?? string.Empty;
        }

        public static string RemoveExtraSpaces(this string str)
        {
            //string propValue = x.GetType()?.GetProperty(propName)?.GetValue(x, null)?.ToString() ?? string.Empty;
            var values = str.Split(' ').Where(s => !string.IsNullOrWhiteSpace(s));
            var value = string.Join(" ", values);
            return value;
        }
    }
}