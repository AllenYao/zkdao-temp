﻿using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
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
                    } catch (DbEntityValidationException ex) {
                        //StringBuilder EFvaliError = new StringBuilder();
                        //foreach (var eve in ex.EntityValidationErrors) {
                        //    EFvaliError.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors: \n", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        //    foreach (var ve in eve.ValidationErrors) {
                        //        EFvaliError.AppendFormat("- Property: \"{0}\", Error: \"{1}\" \n", ve.PropertyName, ve.ErrorMessage);
                        //    }
                        //}

                        //if (ExceptionPolicy.HandleException(ex, "Policy")) throw;
                        throw ex;
                    } catch (DbUpdateException ex) {
                        throw ex.InnerException;
                    } catch (Exception ex) {
                        throw ex;
                    }
                }
            }
        }

        public override void Rollback() {
            Committed = false;
        }

        protected override void DisposeCustom() {
            if (!Committed)
                Commit();
            ctx.Dispose();
        }

        #region IEntityFrameworkRepositoryContext Members

        public DbContext Context {
            get { return ctx; }
        }

        #endregion IEntityFrameworkRepositoryContext Members
    }
}