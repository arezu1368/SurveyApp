using Survey.Common.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Survey.Common.BusinessLayer.User;

namespace Survey.Api.Models.User
{
    public class UserDTO:BaseDTO<Common.BusinessLayer.User.User>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public UserDTO(Common.BusinessLayer.User.User domainEntity = null) 
            : base(domainEntity)
        {
        }

        public override bool FillFromDomainEntity(Common.BusinessLayer.User.User domainEntity)
        {
            if (!base.FillFromDomainEntity(domainEntity))
                return false;
            this.Email = domainEntity.Email;
            this.UserName = domainEntity.Username;
            this.Fullname = domainEntity.FullName;
            return true;
        }

        public override Common.BusinessLayer.User.User MakeFilledDomainEntity()
        {
            UserManager userManager = new UserManager();
            var user = userManager.MakeUserInstance();
            user.Id = this.Id;
            user.FullName = this.Fullname;
            user.Username = this.UserName;
            user.Email = this.Email;
            return user;
        }

    }
}