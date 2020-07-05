using System;
using System.ComponentModel.DataAnnotations;

namespace Recodme.RD.BoraNow.DataLayer.Base
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        private bool _isDeleted;
        public bool IsDeleted
        {
            get
            {
                return _isDeleted;
            }
            set
            {
                _isDeleted = value;
                RegisterChange();
            }
        }

        public virtual void RegisterChange()
        {
            UpdateAt = DateTime.UtcNow;
        }

        public Entity()
        {
            Id = Guid.NewGuid();
            CreateAt = DateTime.UtcNow;
            UpdateAt = CreateAt;
            _isDeleted = false;
        }
        public Entity(Guid id, DateTime createAt, DateTime updateAt, bool isDeleted)
        {
            Id = id;
            CreateAt = createAt;
            UpdateAt = updateAt;
            _isDeleted = isDeleted;
        }
    }
}
