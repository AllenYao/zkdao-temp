using System.Data.Entity;

namespace zkdao.Repositories.EF {

    public class EFDbContextInitializer : DropCreateDatabaseIfModelChanges<EFDbContext> {

        protected override void Seed(EFDbContext context) {
            base.Seed(context);
        }

        public static void Initialize() {
            Database.SetInitializer<EFDbContext>(new EFDbContextInitializer());
        }
    }
}