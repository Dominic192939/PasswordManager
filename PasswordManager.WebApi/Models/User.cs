using PasswordManager.Common.Contracts;

namespace PasswordManager.WebApi.Models
{
    /// <summary>
    /// This model represents a transmission model for the 'User' data unit.
    /// </summary>
    public partial class User : ModelObject, PasswordManager.Common.Contracts.IUser
    {
        public int Age { get; set; }
        public string Nickname { get; set; }
        public string Identity { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Iterations { get; set; }
        public string KeyAlgorithm { get; set; }
        public byte[] PublicKey { get; set; }

        public void AfterCopyProperties(IUser other)
        {
            throw new NotImplementedException();
        }

        public void BeforeCopyProperties(IUser other, ref bool handled)
        {
            throw new NotImplementedException();
        }
    }
}