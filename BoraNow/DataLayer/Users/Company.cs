using Recodme.RD.BoraNow.DataLayer.Base;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.RD.BoraNow.DataLayer.Users
{
    public class Company : NamedEntity
    {
        private string _representative;
        public string Representative
        {
            get
            {
                return _representative;
            }
            set
            {
                _representative = value;
                RegisterChange();
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
                RegisterChange();
            }
        }

        private string _vatNumber;
        public string VatNumber
        {
            get
            {
                return _vatNumber;
            }
            set
            {
                _vatNumber = value;
                RegisterChange();
            }
        }

        //[ForeignKey("Profile")]
        //public Guid ProfileId { get; set; }
        //public virtual Profile Profile { get; set; }

        public virtual ICollection<InterestPoint> InterestPoints { get; set; }

        public Company(string name, string representative, string phoneNumber, string vatNumber) : base(name)
        {
            _representative = representative;
            _phoneNumber = phoneNumber;
            _vatNumber = vatNumber;
            //ProfileId = profileId;
        }

        public Company(Guid id, DateTime createAt, DateTime updateAt, bool isDeleted, string name, string representative, string phoneNumber, string vatNumber) : base(id, createAt, updateAt, isDeleted, name)
        {
            _representative = representative;
            _phoneNumber = phoneNumber;
            _vatNumber = vatNumber;
            //ProfileId = profileId;
        }
    }
}
