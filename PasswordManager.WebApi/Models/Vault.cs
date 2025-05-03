namespace PasswordManager.WebApi.Models
{
    using PasswordManager.Common.Contracts;
    using System;
    /// <summary>
    /// This model represents a Transmission model for the 'Vault' data unit.
    /// </summary>
    public class Vault : ModelObject, PasswordManager.Common.Contracts.IVault
    {
        public string Name { get; set; }
        public IUser Owner { get; set; }
        public IVaultEntry[] Entries { get; set; }

        public void AfterCopyProperties(IVault other)
        {
            throw new NotImplementedException();
        }

        public void BeforeCopyProperties(IVault other, ref bool handled)
        {
            throw new NotImplementedException();
        }
    }
}
