namespace PasswordManager.Logic.Entities
{
    [Table("Vault")]
    public class Vault : EntityObject
    {
        [Required]
        public User Owner { get; set; } = new();

        [MaxLength(100)]
        [Required]
        public string Name { get; set; } = string.Empty;

        public VaultEntry[] Entries { get; set; } = [];
    }
}