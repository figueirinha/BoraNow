using Recodme.RD.BoraNow.DataLayer.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Users
{
    public class ProfileViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string PhotoPath { get; set; }
        public Guid CountryId { get; set; }
        public Guid VisitorId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid UserId { get; set; }

        public Profile ToProfile()
        {
            return new Profile(Description, PhotoPath, CountryId, VisitorId, CompanyId, UserId);
        }

        public static ProfileViewModel Parse(Profile profile)
        {
            return new ProfileViewModel()
            {
                Id = profile.Id,
                Description = profile.Description,
                PhotoPath = profile.PhotoPath,
                CompanyId = profile.CompanyId,
                CountryId = profile.CountryId,
                VisitorId = profile.VisitorId,
                UserId = profile.UserId
            };
        }
    }
}
