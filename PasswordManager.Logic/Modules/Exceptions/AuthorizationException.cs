namespace PasswordManager.Logic.Modules.Exceptions
{
    /// <summary>
    /// Represents errors that occur during application execution.
    /// </summary>
    public partial class AuthorizationException : LogicException
    {
        /// <summary>
        /// Initializes a new instance of the AuthorizationException class with a specified error message.
        /// </summary>
        /// <param name="errorId">Identification of the error message.</param>
        public AuthorizationException(string message, Exception ex)
        : base(message, ex)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the AuthorizationException class with a specified error message.
        /// </summary>
        /// <param name="errorId">Identification of the error message.</param>
        /// <param name="message">The message that describes the error.</param>
        public AuthorizationException(string message)
        : base(message)
        {
        }
    }
}