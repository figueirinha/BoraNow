using Recodme.RD.BoraNow.DataLayer.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recodme.RD.BoraNow.DataLayer.Users
{
    public class Country : NamedEntity
    {
        public virtual ICollection<Profile> Profiles { get; set; }
        public Country(string name) : base(name)
        {
        }

        public Country(Guid id, DateTime createAt, DateTime updateAt, bool isDeleted, string name) : base(id, createAt, updateAt, isDeleted, name)
        {
        }
    }
}
