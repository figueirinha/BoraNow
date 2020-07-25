using Recodme.RD.BoraNow.DataLayer.Base;
using Recodme.RD.BoraNow.DataLayer.Feedbacks;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.RD.BoraNow.DataLayer.Users
{
    public class Visitor : Entity
    {
        private string _firstName;

        [Required]
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                RegisterChange();
            }
        }

        [Required]
        private string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                RegisterChange();
            }
        }

        [Required]
        private DateTime _birthDate;
        public DateTime BirthDate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                _birthDate = value;
                RegisterChange();
            }
        }

        private string _gender;
        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
                RegisterChange();
            }
        }

        [ForeignKey("Profile")]
        public Guid ProfileId { get; set; }
        public virtual Profile Profile { get; set; }


        [ForeignKey("Country")]
        public Guid CountryId { get; set; }
        public virtual Country Country { get; set; }


        public Visitor(string firstName, string lastName, DateTime birthDate, string gender, Guid profileId, Guid countryId) : base()
        {
            _firstName = firstName;
            _lastName = lastName;
            _birthDate = birthDate;
            _gender = gender;
            ProfileId = profileId;
            CountryId = countryId;
        }

        public Visitor(Guid id, DateTime createAt, DateTime updateAt, bool isDeleted, string firstName, string lastName, DateTime birthDate, string gender, Guid profileId, Guid countryId) : base(id, createAt, updateAt, isDeleted)
        {
            _firstName = firstName;
            _lastName = lastName;
            _birthDate = birthDate;
            _gender = gender;
            ProfileId = profileId;
            CountryId = countryId;
        }
    }
}
