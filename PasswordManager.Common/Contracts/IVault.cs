namespace PasswordManager.Common.Contracts
{
    public interface IVault : IIdentifiable
    {
        string Name { get; set; }
        IUser Owner { get; set; }
        IVaultEntry[] Entries { get; set; }
        /// <summary>
        /// Copies the properties of another object to this instance.
        /// </summary>
        /// <param name="other">The object to copy the properties from.</param>
        void CopyProperties(IVault other)
        {
            bool handled = false;
            BeforeCopyProperties(other, ref handled);
            if (handled == false)
            {
                Guid = other.Guid;
                Name = other.Name;
            }
            AfterCopyProperties(other);
        }
        /// <summary>
        /// This method is called before copying the properties of another object to the current instance.
        /// </summary>
        /// <param name="other">The object to copy the properties from.</param>
        /// <param name="handled">A boolean value that indicates whether the method has been handled.</param>
        void BeforeCopyProperties(IVault other, ref bool handled);
        /// <summary>
        /// This method is called after copying properties from another instance of the class.
        /// </summary>
        /// <param name="other">The other instance of the class from which properties were copied.</param>
        void AfterCopyProperties(IVault other);
    }
}
