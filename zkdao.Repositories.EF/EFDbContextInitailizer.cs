using System.Data.Entity;

namespace zkdao.Repositories.EF {

    public class EFDbContextInitializer : CreateDatabaseIfNotExists<EFDbContext> {

        protected override void Seed(EFDbContext context) {
            base.Seed(context);
        }

    }
}