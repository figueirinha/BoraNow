using Recodme.RD.BoraNow.DataLayer.Base;
using Recodme.RD.BoraNow.DataLayer.Feedbacks;
using Recodme.RD.BoraNow.DataLayer.Newsletters;
using Recodme.RD.BoraNow.DataLayer.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.RD.BoraNow.DataLayer.Quizzes
{
    public class InterestPoint : NamedEntity
    {
        private string _description;

        [Required]
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
        private string _address;

        [Required]
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
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
        private string _openingHours;

        [Required]
        public string OpeningHours
        {
            get
            {
                return _openingHours;
            }
            set
            {
                _openingHours = value;
                RegisterChange();
            }
        }
        private string _closingHours;

        [Required]
        public string ClosingHours
        {
            get
            {
                return _closingHours;
            }
            set
            {
                _closingHours = value;
                RegisterChange();
            }
        }
        private string _closingDays;
        public string ClosingDays
        {
            get
            {
                return _closingDays;
            }
            set
            {
                _closingDays = value;
                RegisterChange();
            }
        }
        private bool _covidSafe;

        [Required]
        public bool CovidSafe
        {
            get
            {
                return _covidSafe;
            }
            set
            {
                _covidSafe = value;
                RegisterChange();
            }
        }
        private bool _status;

        [Required]
        public bool Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                RegisterChange();
            }
        }
        [ForeignKey("Company")]
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<InterestPointCategoryInterestPoint> CategoryInterestPoints { get; set; }
        public virtual ICollection<ResultInterestPoint> InterestPointResults { get; set; } 
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<InterestPointNewsletter> InterestPointNewsletters { get; set; }
        
        public InterestPoint(string name, string description, string address, string photoPath, string openingHours, 
            string closingHours, string closingDays, bool covidSafe, bool status, Guid companyId) : base(name)
        {
            _description = description;
            _address = address;
            _photoPath = photoPath;
            _openingHours = openingHours;
            _closingHours = closingHours;
            _closingDays = closingDays;
            _covidSafe = covidSafe;
            _status = status;
            CompanyId = companyId;
        }

        public InterestPoint(Guid id, DateTime createAt, DateTime updateAt, bool isDeleted, string name, string description, 
            string address, string photoPath, string openingHours, string closingHours, string closingDays, bool covidSafe, 
            bool status, Guid companyId) : base(id, createAt, updateAt, isDeleted, name)
        {
            _description = description;
            _address = address;
            _photoPath = photoPath;
            _openingHours = openingHours;
            _closingHours = closingHours;
            _closingDays = closingDays;
            _covidSafe = covidSafe;
            _status = status;
            CompanyId = companyId;
        }
    }
}
