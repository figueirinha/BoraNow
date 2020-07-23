using Recodme.RD.BoraNow.DataLayer.Users;
using System;
using System.ComponentModel.DataAnnotations;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Users
{
    public class VisitorViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Insert your first name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Insert your last name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Insert your birthdate")]
        [Display(Name = "Birthdate")]
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        //public Guid ProfileId { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public string BirthDateToString
        {
            get
            {
                return $"{BirthDate.Day}-{BirthDate.Month}-{BirthDate.Year}";
            }
        }

        public Visitor ToVisitor()
        {
            return new Visitor(FirstName, LastName, BirthDate, Gender/*, ProfileId*/);
        }

        public static VisitorViewModel Parse(Visitor visitor)
        {
            return new VisitorViewModel()
            {
                Id = visitor.Id,
                FirstName = visitor.FirstName,
                LastName = visitor.LastName,
                BirthDate = visitor.BirthDate,
                Gender = visitor.Gender
                //ProfileId = visitor.ProfileId
            };
        }
    }
}
