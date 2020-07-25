using Recodme.RD.BoraNow.DataLayer.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Users
{
    public class ProfileViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Input a description")]
        public string Description { get; set; }

        [Display(Name = "Picture")]
        public string PhotoPath { get; set; }


        public Profile ToProfile()
        {
            return new Profile(Description, PhotoPath);
        }

        public static ProfileViewModel Parse(Profile profile)
        {
            return new ProfileViewModel()
            {
                Id = profile.Id,
                Description = profile.Description,
                PhotoPath = profile.PhotoPath            };
        }
    }
}
