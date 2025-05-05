using System.Text.RegularExpressions;
using System.Xml.Linq;
using PasswordManager.Logic.Modules.Exceptions; 

namespace PasswordManager.Logic.Entities
{
    partial class User
    {
        /// <summary>
        /// Validates essential business rules for the User entity.
        /// Throws BusinessRuleException if any rule is violated.
        /// </summary>
        public void Validate()
        {
            // U1: Identity must be a valid email address
            if (!IsValidEmail(Identity))
            {
                throw new BusinessRuleException("Identity must be a valid email address.");
            }
            // U2: Identity must not be empty
            if (string.IsNullOrWhiteSpace(Identity))
            {
                throw new BusinessRuleException("Identity must not be empty.");
            }
            // U3: PasswordHash must be at least 8 characters long
            if (string.IsNullOrWhiteSpace(PasswordHash) || PasswordHash.Length < 8)
            {
                throw new BusinessRuleException("Master password must be at least 8 characters long.");
            }
            // U4: Nickname must have at least 3 characters if provided
            if (!string.IsNullOrWhiteSpace(Nickname) && Nickname.Trim().Length < 3)
            {
                throw new BusinessRuleException("Nickname must have at least 3 non-whitespace characters.");
            }
            // U5: Age must be at least 13
            if (Age < 13)
            {
                throw new BusinessRuleException("User must be at least 13 years old.");
            }
            // U6: PublicKey must not be null or empty
            if (PublicKey is null || PublicKey.Length == 0)
            {
                throw new BusinessRuleException("Public key must not be empty.");
            }
        }

        private bool IsValidEmail(string identity)
        {
            var result = false;

            if (!string.IsNullOrWhiteSpace(identity))
            {
                string model = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

                result = Regex.IsMatch(identity, model);
            }
            return result;
        }
    }
}
