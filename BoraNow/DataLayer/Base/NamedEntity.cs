using System;
using System.ComponentModel.DataAnnotations;

namespace Recodme.RD.BoraNow.DataLayer.Base
{
    public abstract class NamedEntity : Entity
    {
        private string _name;

        [Required]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                RegisterChange();
            }
        }
        public NamedEntity(string name) : base()
        {
            _name = name;
        }

        public NamedEntity(Guid id, DateTime createAt, DateTime updateAt, bool isDeleted, string name) : base(id, createAt, updateAt, isDeleted)
        {
            _name = name;
        }
    }
}
