using PasswordManager.Common.Contracts;
using PasswordManager.Logic.Contracts;

namespace PasswordManager.Logic.Entities
{
    [Table("MockEntries")]
    public class MockVaultEntry : EntityObject, IMockVaultEntry
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        #region navigation properties
        public string MockUserUserName { get; set; } = string.Empty;
        public MockUser? MockUser { get; set; }
        #endregion navigation properties

        public void AfterCopyProperties(IMockVaultEntry other) { }

        public void BeforeCopyProperties(IMockVaultEntry other, ref bool handled) { }
    }
}