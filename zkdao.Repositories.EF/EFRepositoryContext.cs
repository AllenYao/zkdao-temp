using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using zic_dotnet.Repositories;

namespace zkdao.Repositories.EF {
    public class EFRepositoryContext : RepositoryContext {
        private readonly EFDbContext ctx = new EFDbContext();
        private readonly object sync = new object();

        public override void RegisterDeleted<TAggregateRoot>(TAggregateRoot obj) {
            ctx.Entry<TAggregateRoot>(obj).State = System.Data.EntityState.Deleted;
            Committed = false;
        }

        public override void RegisterModified<TAggregateRoot>(TAggregateRoot obj) {
            ctx.Entry<TAggregateRoot>(obj).State = System.Data.EntityState.Modified;
            Committed = false;
        }

        public override void RegisterNew<TAggregateRoot>(TAggregateRoot obj) {
            ctx.Entry<TAggregateRoot>(obj).State = System.Data.EntityState.Added;
            Committed = false;
        }

        public override void Commit() {
            if (!Committed) {
                lock (sync) {
                    try {
                        ctx.SaveChanges();
                        Committed = true;
                    } catch (DbEntityValidationException e) {
                        foreach (var eve in e.EntityValidationErrors) {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors) {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                    }
                }
            }
        }

        public override void Rollback() {
            Committed = false;
        }

        protected override void DisposeDeHostobj() {
            if (!Committed)
                Commit();
            ctx.Dispose();
        }

        #region IEntityFrameworkRepositoryContext Members

        public DbContext Context {
            get { return ctx; }
        }

        #endregion
    }
}
