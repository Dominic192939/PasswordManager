using PasswordManager.Common.Contracts;
using System.Security.Cryptography;

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
        public string Permissions { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }

        public void AfterCopyProperties(ISharedVaultAccess other)
        {
        }

        public void BeforeCopyProperties(ISharedVaultAccess other, ref bool handled)
        {
        }
    }
}