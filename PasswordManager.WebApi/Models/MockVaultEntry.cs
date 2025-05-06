using PasswordManager.Common.Contracts;

namespace PasswordManager.WebApi.Models
{
    /// <summary>
    /// This model represents a transmission model for the 'MockVaultEntry' data unit.
    /// </summary>
    public class MockVaultEntry : ModelObject, Common.Contracts.IMockVaultEntry
    {
        public string Name { get; set; } // Name des Services (z.B. "Google")
        public string Url { get; set ; } // URL des Services (z.B. "https://www.google.com")
        public string UserName { get; set; } // Username des Services
        public string Email { get; set; } // E-Mail-Adresse des Services
        public string Password { get; set; } // Passwort des Services
        public string MockUserUserName { get; set; } // Username des Mock-Users (Der dazugehörige User zum VaultEntry)

        public DateTime CreatedAt { get; set; } // Erstellungsdatum des Eintrags
        public DateTime ModifiedAt { get; set; } // Änderungsdatum des Eintrags

        public void AfterCopyProperties(IMockVaultEntry other) { }

        public void BeforeCopyProperties(IMockVaultEntry other, ref bool handled){ }
    }
}
