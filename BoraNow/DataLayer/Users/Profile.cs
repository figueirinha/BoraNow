using Recodme.RD.BoraNow.DataLayer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.RD.BoraNow.DataLayer.Users
{
    public class Profile : Entity
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
  
        private string _photoPath;
        public string PhotoPath
        {
            get
            {
                return _photoPath;
            }
            set
            {
                _photoPath = value;
                RegisterChange();
            }
        }

        [ForeignKey("Country")]
        public Guid CountryId { get; set; }
        public virtual Country Country { get; set; }

        [ForeignKey("Visitor")]
        public Guid VisitorId { get; set; }
        public virtual Visitor Visitor { get; set; }

        [ForeignKey("Company")]
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Profile(string description, string photoPath, Guid countryId, Guid visitorId, Guid companyId, Guid userId) : base()
        {
            _description = description;
            _photoPath = photoPath;
            CountryId = countryId;
            VisitorId = visitorId;
            CompanyId = companyId;
            UserId = userId;
        }

        public Profile(Guid id, DateTime createAt, DateTime updateAt, bool isDeleted, string description, string photoPath, Guid countryId, Guid visitorId, Guid companyId, Guid userId) : base(id, createAt, updateAt, isDeleted)
        {
            _description = description;
            _photoPath = photoPath;
            CountryId = countryId;
            VisitorId = visitorId;
            CompanyId = companyId;
            UserId = userId;
        }
    }
}
