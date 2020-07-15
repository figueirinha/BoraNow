using Recodme.RD.BoraNow.DataLayer.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Recodme.RD.BoraNow.DataLayer.Newsletters
{
    public class Newsletter : Entity
    {
        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                RegisterChange();
            }
        }
        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                RegisterChange();
            }
        }
        public virtual ICollection<InterestPointNewsletter> InterestPointNewsletters { get; set; }

        public Newsletter(string description, string title)
        {
            _description = description;
            _title = title;
        }

        public Newsletter(Guid id, DateTime createAt, DateTime updateAt, bool isDeleted, string description, string title) : base(id, createAt, updateAt, isDeleted)
        {
            _description = description;
            _title = title;
        }

    }
}
