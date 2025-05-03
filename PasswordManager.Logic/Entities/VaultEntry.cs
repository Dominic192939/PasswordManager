namespace PasswordManager.Logic.Entities
{
    [Table("VaultEntry")]
    public partial class VaultEntry : EntityObject
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Url { get; set; } = string.Empty;

        [Required]
        public byte[] EncryptedData { get; set; } = [];

        [Required]
        public byte[] Nonce { get; set; } = [];

        [Required]
        public byte[] Tag { get; set; } = [];

        [Required]
        public CommonEnums.VaultEntryType Type { get; set; }
    }
}