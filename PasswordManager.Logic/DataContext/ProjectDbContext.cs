using Microsoft.EntityFrameworkCore;
using PasswordManager.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Logic.DataContext
{
    internal sealed class ProjectDbContext : DbContext, IContext
    {
        #region fields
        /// <summary>
        /// The type of the database (e.g., "Sqlite", "SqlServer").
        /// </summary>
        private static readonly string _databaseType = "Postgres";
        /// <summary>
        /// The connection string for the database.
        /// </summary>
        private static readonly string _connectionString = "Host=localhost;Port=5432;Username=postgres;Password=passme1234!;Database=PasswordManagerDb";
        #endregion fields

        #region properties
        public string? SessionToken { set; get; }
        public DbSet<Entities.MockUser> MockUsersSet { get; set; }
        public DbSet<Entities.MockVaultEntry> MockVaultEntriesSet { get; set; }
        #endregion properties

        #region constructors
        static ProjectDbContext()
        {
            var appSettings = Common.Modules.Configuration.AppSettings.Instance;

            _databaseType = appSettings["Database:Type"] ?? _databaseType;
            _connectionString = appSettings[$"ConnectionStrings:{_databaseType}ConnectionString"] ?? _connectionString;
        }
        #endregion constructors

        #region overrides
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_databaseType == "Postgres")
            {
                optionsBuilder.UseNpgsql(_connectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }
        #endregion overrides

        #region methods
        public int RejectChanges()
        {
            int count = 0;

            foreach (var entry in ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        count++;
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        count++;
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        count++;
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
            return count;
        }

        public Task<int> RejectChangesAsync()
        {
            return Task.Run(() => RejectChanges());
        }
        #endregion methods
    }
}
