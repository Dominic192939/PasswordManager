namespace PasswordManager.Logic.Contracts
{
    public interface IContext : IDisposable
    {
        #region properties
        /// <summary>
        /// Sets the session token.
        /// </summary>
        string SessionToken { set; }
        public DbSet<Entities.MockUser> MockUsersSet { get; set; }
        public DbSet<Entities.MockVaultEntry> MockVaultEntriesSet { get; set; }
        #endregion properties

        #region methods
        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>The number of state entries written to the underlying database.</returns>
        int SaveChanges();

        /// <summary>
        /// Rejects all changes made in this context, reverting entities to their original state.
        /// </summary>
        /// <returns>The number of state entries reverted to their original state.</returns>
        int RejectChanges();
        /// <summary>
        /// Asynchronously rejects all changes made in this context, reverting entities to their original state.
        /// </summary>
        /// <returns>A task that represents the asynchronous reject operation. The task result contains the number of state entries reverted to their original state.</returns>
        Task<int> RejectChangesAsync();
        #endregion methods
    }
}