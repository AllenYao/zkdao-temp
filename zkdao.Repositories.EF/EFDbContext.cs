using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using zkdao.Domain;

namespace zkdao.Repositories.EF {

    public sealed class EFDbContext : DbContext {

        public EFDbContext()
            : base(ConfigurationManager.AppSettings["EFdatabase"]) {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Info> Infos { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<ReplyChild> ReplyChilds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}