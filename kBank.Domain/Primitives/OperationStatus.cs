using System;
using System.Diagnostics;

namespace kBank.Domain.Primitives
{
    [DebuggerDisplay("Status: {Status}")]
    public class OperationStatus<T>
        where T : class
    {
        public T Result { get; set; }
        public int RecordsAffected { get; set; }
        public string Message { get; set; }
        public Object OperationId { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
        public string ExceptionInnerMessage { get; set; }
        public string ExceptionInnerStackTrace { get; set; }

        private OperationStatus()
        {

        }

        public bool Succeeded { get; set; }

        public static OperationStatus<T> CreateNotFound()
        {
            return new OperationStatus<T> { Result = null, RecordsAffected = 0, Succeeded = false };
        }

        public static OperationStatus<T> CreateFromResult(T result, int? recordsAffected = null)
        {
            return new OperationStatus<T> { Result = result, Succeeded = true };
        }

        public static OperationStatus<T> CreateFromFailure(string message)
        {
            var opStatus = new OperationStatus<T>
            {
                Message = message,
                OperationId = null,
                Succeeded = false
            };

            return opStatus;
        }

        public static OperationStatus<T> CreateFromException(string message, Exception ex)
        {
            var opStatus = new OperationStatus<T>
            {
                Message = message,
                OperationId = null,
                Succeeded = false
            };

            if (ex != null)
            {
                opStatus.ExceptionMessage = ex.Message;
                opStatus.ExceptionStackTrace = ex.StackTrace;
                opStatus.ExceptionInnerMessage = (ex.InnerException == null) ? null : ex.InnerException.Message;
                opStatus.ExceptionInnerStackTrace = (ex.InnerException == null) ? null : ex.InnerException.StackTrace;
            }
            return opStatus;
        }

    }

    [DebuggerDisplay("Status: {Status}")]
    public class OperationStatus
    {
        public int? RecordsAffected { get; set; }
        public string Message { get; set; }
        public Object OperationId { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
        public string ExceptionInnerMessage { get; set; }
        public string ExceptionInnerStackTrace { get; set; }
        public bool Succeeded { get; set; }

        public static OperationStatus CreateFromException(string message, Exception ex)
        {
            var opStatus = new OperationStatus
            {
                Message = message,
                OperationId = null,
            };

            if (ex != null)
            {
                opStatus.ExceptionMessage = ex.Message;
                opStatus.ExceptionStackTrace = ex.StackTrace;
                opStatus.ExceptionInnerMessage = (ex.InnerException == null) ? null : ex.InnerException.Message;
                opStatus.ExceptionInnerStackTrace = (ex.InnerException == null) ? null : ex.InnerException.StackTrace;
            }
            return opStatus;
        }
    }
}
