using Bsn.Common.BusinessLayer.Exceptions;
using Survey.Common.BusinessLayer.Enums;
using Survey.Common.BusinessLayer.Exceptions;
using Survey.Common.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Common.BusinessLayer
{
    public abstract class DomainEntity<TContext, TEntity> : IDomainEntity
         where TContext : DbContext
         where TEntity: IEntity

    {
        protected DomainEntity(TContext context, IEntity entity)
        {
            this.Context = context;
            this.entity = entity;
        }
        private IEntity entity;
        protected TContext Context
        {
            get;
            private set;
        }
        protected virtual object[] GetPrimaryValues()
        {
            List<object> objs = new List<object>();
            objs.Add(this.Id);
            return objs.ToArray();
        }
        protected TEntity Entity
        {
            get
            {
                return (TEntity)entity;
            }
            set
            {
                entity = value;

            }
        }
        protected virtual void SetEntity(IEntity entity)
        {
            this.entity = entity;
        }
        protected virtual void Commit(ref List<ServerError> errors)
        {
        }
        protected virtual DomainEntity<TContext, TEntity> Add(ref List<ServerError> errors)
        {
            try
            {
                this.Commit(ref errors);
                if (errors.Count > 0)
                    return null;
                this.Context.Entry((IEntity)this.Entity).State = System.Data.Entity.EntityState.Added;
                this.Context.SaveChanges();
                return this;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected virtual DomainEntity<TContext, TEntity> Edit(ref List<ServerError> errors)
        {
            try
            {
                this.Commit(ref errors);
                if (errors.Count > 0)
                    return null;
                //this.Context.Entry(this.Entity).State = System.Data.Entity.EntityState.Modified;
                var st = this.Context.Entry((IEntity)this.Entity).State;
                this.Context.SaveChanges();
                return this;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Id
        {
            get
            {
                return this.Entity.Id;
            }
            set
            {
                this.Entity.Id = value;
                if (value > 0)
                    this.State = States.Updated;
            }
        }
        public States State { get; set; }
        public virtual Boolean Save(ref List<ServerError> errors)
        {
            if (errors == null)
                errors = new List<ServerError>();
            try
            {
                DomainEntity<TContext, TEntity> de = null;
                if (this.State == States.Updated)
                {

                    de = this.Edit(ref errors);
                }
                else
                    de = this.Add(ref errors);
                return de != null;
            }
            catch (Exception exc)
            {
                if (exc is DbUpdateException)
                {
                    var exc1 = (DbUpdateException)exc;
                    var exc2 = exc1.GetBaseException();

                    SqlException sqlExc = exc2 as SqlException;
                    if (sqlExc != null)
                    {
                        var errs = DbExceptionHandler.GetInstance().GetErrors((SqlException)sqlExc);
                        errs.ForEach(a => a.Obj = this);
                        errors.AddRange(errs);
                    }
                }

                //if (exc is DbEntityValidationException)
                //{

                //    DbEntityValidationException valExc = exc as DbEntityValidationException;

                //}

                errors.Add(new UnHandledException() { Obj = this, ThrownException = exc });
                return false;
            }

        }

        public virtual bool FillFromContext()
        {
            var obj = this.Context.Set(this.Entity.GetType()).Find(this.GetPrimaryValues());
            if (obj != null)
                this.Entity = (TEntity)obj;
            return obj != null;
        }
    }
}
