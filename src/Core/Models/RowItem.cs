namespace Core.Models
{
    public class RowItem
    {
        public string ColumnName { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public bool IsDedupeField { get; set; } = false;

    }
}