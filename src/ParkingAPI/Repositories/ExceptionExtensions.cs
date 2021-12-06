using System;

namespace Parking.API.Repositories
{
    public static class ExceptionExtensions
    {
        public static string ErrorMessage(this Exception exception)
        {
            var msg = exception.Message;
            if (exception.InnerException != null)
            {
                msg += " " + ErrorMessage(exception.InnerException);
            }

            return msg;
        }
        }
}
