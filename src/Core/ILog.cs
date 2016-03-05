using System;
using Core.Exceptions;

namespace Core
{
    public interface ILog
    {
        void Log(Exception ex);
        void Log(Exception ex, string message);
        void Log(InvalidColumnException ex);
    }
}