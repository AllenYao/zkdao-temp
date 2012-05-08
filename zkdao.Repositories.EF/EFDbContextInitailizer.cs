using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace zkdao.Repositories.EF
{
    public class EFDbContextInitializer : DropCreateDatabaseIfModelChanges<EFDbContext> {
        protected override void Seed(EFDbContext context) {

        }
    }
}
