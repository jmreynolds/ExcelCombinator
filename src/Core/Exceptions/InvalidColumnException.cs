using System;

namespace Core.Exceptions
{
    public class InvalidColumnException : Exception
    {
        public string InvalidColumnName { get; }
        public string[] AllowedColumns { get; }

        public InvalidColumnException(string message, string invalidColumnName, string[] allowedColumns, Exception innerException)
            :base(message, innerException)
        {
            InvalidColumnName = invalidColumnName;
            AllowedColumns = allowedColumns;
        }
    }
}