namespace PasswordManager.Logic.Entities
{
    partial class User
    {
        [MaxLength(100)]
        [Required]
        public string Identity { get; set; } = string.Empty;

        [MaxLength(256)]
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [MaxLength(256)]
        [Required]
        public string PasswordSalt { get; set; } = string.Empty;

        [Required]
        public int Iterations { get; set; }

        [MaxLength(128)]
        [Required]
        public string KeyAlgorithm { get; set; } = string.Empty;

        [Required]
        public byte[] PublicKey { get; set; } = [];
    }
}