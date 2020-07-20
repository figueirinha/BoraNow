using Microsoft.AspNetCore.Identity;
using Recodme.RD.BoraNow.DataLayer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.RD.BoraNow.DataLayer.Users
{
    public class User : IdentityUser<Guid>
    {       

        [ForeignKey("Profile")]
        public Guid ProfileId { get; set; }
        public virtual Profile Profile { get; set; }

        //[ForeignKey("Role")]
        //public Guid RoleId { get; set; }
        //public virtual Role Role { get; set; }
        
    }
}
