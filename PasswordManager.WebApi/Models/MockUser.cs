using PasswordManager.Common.Contracts;

namespace PasswordManager.WebApi.Models
{
    public class MockUser : ModelObject, Common.Contracts.IMockUser
    {
        Guid IIdentifiable.Guid { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AuthToken { get; set; }
        public DateTime AuthTokenGeneratedAt { get; set; }
        public ICollection<IMockVaultEntry> VaultEntries { get; set; }

        public bool IsTokenValid()
        {
            throw new NotImplementedException();
        }
    }
}