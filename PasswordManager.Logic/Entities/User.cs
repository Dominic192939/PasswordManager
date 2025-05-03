namespace PasswordManager.Logic.Entities
{
    [Table("User")]
    public partial class User : EntityObject
    {
        [Required]
        public int Age { get; set; }
        public string Nickname { get; set; } = string.Empty;

    }
}