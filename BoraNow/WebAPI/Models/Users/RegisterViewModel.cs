using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Users
{
    public class RegisterViewModel
    {
        [Display(Name = "About yourself")]
        public string Description { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Input the user name")]
        public string UserName { get; set; }

        [Display(Name = "Photo")]
        public string PhotoPath { get; set; }

        [Display(Name = "Where do you live")]
        [Required(ErrorMessage = "Input country")]
        public Guid CountryId { get; set; }

        [Required(ErrorMessage = "Input the phone number")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Input the phone number")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
