namespace PasswordManager.Logic.Entities
{
    [Table("User")]
    public partial class User : EntityObject
    {
        [Required]
        public int Age { get; set; }
        [MaxLength(64)]
        public string? Nickname { get; set; }
    }
}