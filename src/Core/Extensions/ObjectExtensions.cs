using System.Linq;

namespace Core.Extensions
{
    public static class ObjectExtensions
    {
        public static string GetStringValue(this object x, string propName)
        {
            string propValue = x.GetType().GetProperty(propName)?.GetValue(x, null)?.ToString() ?? string.Empty;
            var values = propValue.Split(' ').Where(s => !string.IsNullOrWhiteSpace(s));
            var value = string.Join(" ", values);
            return value;
        }

    }
}