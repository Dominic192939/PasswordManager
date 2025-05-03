using PasswordManager.Common.Contracts;
using PasswordManager.Logic.Contracts;

namespace PasswordManager.Logic.Entities
{
    [Table("MockUser")]
    public class MockUser : EntityObject, IMockUser
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

        public ICollection<MockVaultEntry> VaultEntries { get; set; } = new List<MockVaultEntry>();

        [NotMapped]
        public string AuthToken { get; set; } = string.Empty;
        [NotMapped]
        public DateTime AuthTokenGeneratedAt { get; set; }

        public bool IsTokenValid()
        {
            return !string.IsNullOrEmpty(AuthToken) && ((DateTime.UtcNow - AuthTokenGeneratedAt).TotalMinutes <= 30);
        }
    }
}
