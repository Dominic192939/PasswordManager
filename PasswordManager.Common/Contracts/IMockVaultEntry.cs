namespace PasswordManager.Common.Contracts
{
    public interface IMockVaultEntry : IIdentifiable
    {
        string Name { get; set; }
        string? Url { get; set; }
        string? UserName { get; set; }
        string? Email { get; set; }
        string Password { get; set; }
        string? MockUserUserName { get; set; }
        /// <summary>
        /// Copies the properties of another object to this instance.
        /// </summary>
        /// <param name="other">The object to copy the properties from.</param>
        void CopyProperties(IMockVaultEntry other)
        {
            bool handled = false;
            BeforeCopyProperties(other, ref handled);
            if (handled == false)
            {
                ((IIdentifiable)this).CopyProperties(other);
                Name = other.Name;
                Url = other.Url;
                UserName = other.UserName;
                Email = other.Email;
                Password = other.Password;
                MockUserUserName = other.MockUserUserName;
            }
            AfterCopyProperties(other);
        }
        /// <summary>
        /// This method is called before copying the properties of another object to the current instance.
        /// </summary>
        /// <param name="other">The object to copy the properties from.</param>
        /// <param name="handled">A boolean value that indicates whether the method has been handled.</param>
        void BeforeCopyProperties(IMockVaultEntry other, ref bool handled);
        /// <summary>
        /// This method is called after copying properties from another instance of the class.
        /// </summary>
        /// <param name="other">The other instance of the class from which properties were copied.</param>
        void AfterCopyProperties(IMockVaultEntry other);
    }
}