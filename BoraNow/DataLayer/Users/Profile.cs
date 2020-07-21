using Recodme.RD.BoraNow.DataLayer.Base;
using Recodme.RD.BoraNow.DataLayer.Feedbacks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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

      
        public virtual ICollection<Feedback> Feedbacks{ get; set; }

        public virtual ICollection<Company> Companies { get; set; }
       

        public Profile(string description, string photoPath, Guid countryId) : base()
        {
            _description = description;
            _photoPath = photoPath;
            CountryId = countryId;          
        }

        public Profile(Guid id, DateTime createAt, DateTime updateAt, bool isDeleted, string description, string photoPath, Guid countryId) : base(id, createAt, updateAt, isDeleted)
        {
            _description = description;
            _photoPath = photoPath;
            CountryId = countryId;       
        }
    }
}
