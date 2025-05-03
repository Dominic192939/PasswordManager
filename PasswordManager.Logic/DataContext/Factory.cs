using PasswordManager.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Logic.DataContext
{
    /// <summary>
    /// Factory class to create instances of IContext.
    /// </summary>
    public static class Factory
    {
        /// <summary>
        /// Creates an instance of IContext.
        /// </summary>
        /// <returns>An instance of IContext.</returns>
        public static IContext CreateContext()
        {
            var result = new ProjectDbContext();

            return result;
        }

#if DEBUG
        public static void CreateDatabase()
        {
            var context = new ProjectDbContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        public static void InitDatabase()
        {
            var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;
            var context = CreateContext();

            CreateDatabase();

            context.MockVaultEntriesSet.Add(new Entities.MockVaultEntry
            {
                Guid = Guid.NewGuid(),
                Name = "Google",
                UserName = "TestUserName",
                Password = "Test1234",
                Url = "www.google.com",
                Email = "Test@google.com",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
            });

            context.MockVaultEntriesSet.Add(new Entities.MockVaultEntry
            {
                Guid = Guid.NewGuid(),
                Name = "Apple",
                UserName = "AppleUserName",
                Password = "Apple1234",
                Url = "www.apple.com",
                Email = "Test@apple.com",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
            });

            context.SaveChanges();
#endif
        }
    }
}