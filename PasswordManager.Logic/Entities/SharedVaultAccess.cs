using PasswordManager.Common.Contracts;

namespace PasswordManager.Logic.Entities
{
    [Table("SharedVaultAccess")]
    public class SharedVaultAccess : EntityObject, ISharedVaultAccess
    {
        [Required]
        public Vault Vault { get; set; } = new();

        [Required]
        public User Owner { get; set; } = new();

        [Required]
        public User Recipient { get; set; } = new();

        [Required]
        public byte[] EncryptedSharingKey { get; set; } = [];

        [Required]
        public byte[] SharingKeyNonce { get; set; } = [];

        [Required]
        public byte[] SharingKeyTag { get; set; } = [];

        [Required]
        public string Permissions { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }

        public void AfterCopyProperties(ISharedVaultAccess other)
        {
            throw new NotImplementedException();
        }

        public void BeforeCopyProperties(ISharedVaultAccess other, ref bool handled)
        {
            throw new NotImplementedException();
        }
    }
}