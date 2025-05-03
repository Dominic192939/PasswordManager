namespace PasswordManager.Common.Contracts
{
    public interface IIdentifiable
    {
        #region properties
        /// <summary>
        /// Gets the unique identifier for the entity.
        /// </summary>
        Guid Guid { get; protected set; }
        #endregion properties

        #region methods
        void BeforeCopyProperties(IIdentifiable other, ref bool handled);
        void AfterCopyProperties(IIdentifiable other);
        void CopyProperties(IIdentifiable other)
        {
            bool handled = false;
            BeforeCopyProperties(other, ref handled);
            if (handled == false)
            {
                if(other == null)
                    throw new ArgumentNullException(nameof(other));

                // Copies the Guid property from the other instance.
                Guid = other.Guid;
            }
            AfterCopyProperties(other);
        }
        #endregion methods
    }
}
