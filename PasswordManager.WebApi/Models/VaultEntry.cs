namespace PasswordManager.WebApi.Models
{
    using PasswordManager.Common.Contracts;
    using PasswordManager.Common.Enums;
    using System;
    /// <summary>
    /// This model represents a transmission model for the 'VaultEntry' data unit.
    /// </summary>

    public partial class VaultEntry : ModelObject, PasswordManager.Common.Contracts.IVaultEntry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public byte[] EncryptedData { get; set; }
        public byte[] Nonce { get; set; }
        public byte[] Tag { get; set; }
        public VaultEntryType Type { get; set; }
    }
}