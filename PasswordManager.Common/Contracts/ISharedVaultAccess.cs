namespace PasswordManager.Common.Contracts
{
    public partial interface ISharedVaultAccess : IIdentifiable
    {
        byte[] EncryptedSharingKey { get; set; }
        byte[] SharingKeyNonce { get; set; }
        byte[] SharingKeyTag { get; set; }
        string Permissions { get; set; }
        DateTime ExpiresAt { get; set; }

        /// <summary>
        /// Copies the properties of another object to this instance.
        /// </summary>
        /// <param name="other">The object to copy the properties from.</param>
        void CopyProperties(ISharedVaultAccess other)
        {
            bool handled = false;
            BeforeCopyProperties(other, ref handled);
            if (handled == false)
            {
                ((IIdentifiable)this).CopyProperties(other);
                EncryptedSharingKey = other.EncryptedSharingKey;
                SharingKeyNonce = other.SharingKeyNonce;
                SharingKeyTag = other.SharingKeyTag;
                Permissions = other.Permissions;
                ExpiresAt = other.ExpiresAt;
            }
            AfterCopyProperties(other);
        }
        /// <summary>
        /// This method is called before copying the properties of another object to the current instance.
        /// </summary>
        /// <param name="other">The object to copy the properties from.</param>
        /// <param name="handled">A boolean value that indicates whether the method has been handled.</param>
        void BeforeCopyProperties(ISharedVaultAccess other, ref bool handled);
        /// <summary>
        /// This method is called after copying properties from another instance of the class.
        /// </summary>
        /// <param name="other">The other instance of the class from which properties were copied.</param>
        void AfterCopyProperties(ISharedVaultAccess other);
    }
}
