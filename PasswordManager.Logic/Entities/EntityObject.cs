namespace PasswordManager.Logic.Entities
{
    /// <summary>
    /// Represents an abstract base class for entities with an identifier.
    /// </summary>
    public class EntityObject : DbObject, CommonContracts.IIdentifiable
    {
        /// <summary>
        /// Gets or sets the identifier of the entity.
        /// </summary>
        [Key]
        public Guid Guid { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;

        public void AfterCopyProperties(CommonContracts.IIdentifiable other) { }

        public void BeforeCopyProperties(CommonContracts.IIdentifiable other, ref bool handled) { }
    }
}
