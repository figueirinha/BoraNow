using Recodme.RD.BoraNow.DataLayer.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recodme.RD.BoraNow.DataLayer.Users
{
    public class Role : NamedEntity
    {
        public virtual ICollection<User> Users { get; set; }
        
        public Role(string name) : base(name)
        {
        }            

        public Role(Guid id, DateTime createAt, DateTime updateAt, bool isDeleted, string name) : base(id, createAt, updateAt, isDeleted, name)
        {
        }
    }
}
