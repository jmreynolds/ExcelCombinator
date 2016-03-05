using System;
using Core;
using Core.Exceptions;
using Exceptionless;

namespace Infrastructure.Logging
{
    public class Logger : ILog
    {

        public void Log(Exception ex)
        {
            ex.ToExceptionless().Submit();
        }

        public void Log(Exception ex, string message)
        {
            ex.ToExceptionless().Submit();
        }

        public void Log(InvalidColumnException ex)
        {
            ex.ToExceptionless().Submit();
        }
    }
}