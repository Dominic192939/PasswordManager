namespace PasswordManager.Common.Contracts
{
    public partial interface IVaultEntry : IIdentifiable
    {
        string Name { get; set; }
        string Description { get; set; }
        string Url { get; set; }
        byte[] EncryptedData { get; set; }
        byte[] Nonce { get; set; }
        byte[] Tag { get; set; }
        PasswordManager.Common.Enums.VaultEntryType Type { get; set; }

        /// <summary>
        /// Copies the properties of another object to this instance.
        /// </summary>
        /// <param name="other">The object to copy the properties from.</param>
        void CopyProperties(IVaultEntry other)
        {
            bool handled = false;
            BeforeCopyProperties(other, ref handled);
            if (handled == false)
            {
                ((IIdentifiable)this).CopyProperties(other);
                Name = other.Name;
                Description = other.Description;
                Url = other.Url;
                EncryptedData = other.EncryptedData;
                Nonce = other.Nonce;
                Tag = other.Tag;
                Type = other.Type;
            }
            AfterCopyProperties(other);
        }
        /// <summary>
        /// This method is called before copying the properties of another object to the current instance.
        /// </summary>
        /// <param name="other">The object to copy the properties from.</param>
        /// <param name="handled">A boolean value that indicates whether the method has been handled.</param>
        partial void BeforeCopyProperties(IVaultEntry other, ref bool handled);
        /// <summary>
        /// This method is called after copying properties from another instance of the class.
        /// </summary>
        /// <param name="other">The other instance of the class from which properties were copied.</param>
        partial void AfterCopyProperties(IVaultEntry other);
    }
}
