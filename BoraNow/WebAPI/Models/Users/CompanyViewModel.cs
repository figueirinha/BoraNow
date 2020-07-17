using Recodme.RD.BoraNow.DataLayer.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Users
{
    public class CompanyViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Representative { get; set; }
        public string PhoneNumber { get; set; }
        public string VatNumber { get; set; }
        public Guid ProfileId { get; set; }

        public Company ToCompany()
        {
            return new Company(Name,Representative, PhoneNumber, VatNumber, ProfileId);
        }

        public static CompanyViewModel Parse(Company company)
        {
            return new CompanyViewModel()
            {
                Id = company.Id,
                Name = company.Name,
                Representative = company.Representative,
                PhoneNumber = company.PhoneNumber,
                VatNumber = company.VatNumber,
                ProfileId = company.ProfileId
            };
        }
    }
}
