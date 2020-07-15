using Recodme.RD.BoraNow.DataLayer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.RD.BoraNow.DataLayer.Users
{
    public class User : Entity
    {
        private string _email;

        [Required]
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                RegisterChange();
            }
        }

        private string _password;

        [Required]
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RegisterChange();
            }
        }

        public virtual ICollection<Profile> Profiles { get; set; }

        [ForeignKey("Role")]
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }

        public User(string email, string password, Guid roleId) : base()
        {
            _email = email;
            _password = password;
            RoleId = roleId;
        }

        public User(Guid id, DateTime createAt, DateTime updateAt, bool isDeleted, string email, string password, Guid roleId) : base(id, createAt, updateAt, isDeleted)
        {
            _email = email;
            _password = password;
            RoleId = roleId;
        }
    }
}
