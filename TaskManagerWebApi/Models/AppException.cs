using System.Globalization;

namespace TaskManagerWebApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class AppException : Exception
    {

        /// <summary>
        /// 
        /// </summary>
        public AppException() : base() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public AppException(string message) : base(message) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public AppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
