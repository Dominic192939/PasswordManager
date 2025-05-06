using PasswordManager.Common.Contracts;

namespace PasswordManager.Common.Models
{
    public class ModelObject : Contracts.IIdentifiable
    {
        public Guid Guid { get; set; }
        public void AfterCopyProperties(IIdentifiable other)
        {
        }

        public void BeforeCopyProperties(IIdentifiable other, ref bool handled)
        {
        }
    }
}
