namespace PasswordManager.Common.Contracts
{
    public interface IUser : IIdentifiable
    {
        int Age { get; set; }
        string Nickname { get; set; }
        string Identity { get; set; }
        string PasswordHash { get; set; }
        string PasswordSalt { get; set; }
        string Iterations { get; set; }
        string KeyAlgorithm { get; set; }
        byte[] PublicKey { get; set; }
        /// <summary>
        /// Copies the properties of another object to this instance.
        /// </summary>
        /// <param name="other">The object to copy the properties from.</param>
        void CopyProperties(IUser other)
        {
            bool handled = false;
            BeforeCopyProperties(other, ref handled);
            if (handled == false)
            {
                ((IIdentifiable)this).CopyProperties(other);
                Age = other.Age;
                Nickname = other.Nickname;
                Identity = other.Identity;
                PasswordHash = other.PasswordHash;
                PasswordSalt = other.PasswordSalt;
                Iterations = other.Iterations;
                KeyAlgorithm = other.KeyAlgorithm;
                PublicKey = other.PublicKey;
            }
            AfterCopyProperties(other);
        }
        /// <summary>
        /// This method is called before copying the properties of another object to the current instance.
        /// </summary>
        /// <param name="other">The object to copy the properties from.</param>
        /// <param name="handled">A boolean value that indicates whether the method has been handled.</param>
        void BeforeCopyProperties(IUser other, ref bool handled);
        /// <summary>
        /// This method is called after copying properties from another instance of the class.
        /// </summary>
        /// <param name="other">The other instance of the class from which properties were copied.</param>
        void AfterCopyProperties(IUser other);
    }
}
