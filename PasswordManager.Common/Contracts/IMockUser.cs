namespace PasswordManager.Common.Contracts
{
    public interface IMockUser : IIdentifiable
    {
        string UserName { get; set; } 
        string Password { get; set; } 
        string AuthToken { get; set; }
        DateTime AuthTokenGeneratedAt { get; set; }
        public bool IsTokenValid();

        /// <summary>
        /// Copies the properties of another object to this instance.
        /// </summary>
        /// <param name="other">The object to copy the properties from.</param>
        void CopyProperties(IMockUser other)
        {
            bool handled = false;
            BeforeCopyProperties(other, ref handled);
            if (handled == false)
            {
                ((IIdentifiable)this).CopyProperties(other);
                UserName = other.UserName;
                Password = other.Password;
                AuthToken = other.AuthToken;
                AuthTokenGeneratedAt = other.AuthTokenGeneratedAt;
            }
            AfterCopyProperties(other);
        }
    }
}