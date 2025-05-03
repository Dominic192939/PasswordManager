namespace PasswordManager.Logic.Modules.Exceptions
{
    /// <summary>
    /// Represents errors encountered while running the application.
    /// </summary>
    public class LogicException : ApplicationException
    {   
        /// <summary>
        /// Initializes a new instance of the LogicException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public LogicException(string? message)
        : base(message)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the LogicException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">An instance of inner exception.</param>
        public LogicException(string? message, Exception? innerException)
        : base(message, innerException)
        {
        }
    }
}
