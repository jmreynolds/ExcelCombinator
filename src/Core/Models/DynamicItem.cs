namespace Core.Models
{
    public class DynamicItem
    {
        public string ColumnName { get; set; } = string.Empty;
        public dynamic Value { get; set; } = string.Empty;
        public bool ShouldRemoveDupes { get; set; } = false;
        public bool ShouldAggregate { get; set; } = false;
        public bool ShouldIncludeInOutput { get; set; } = false;
    }
}