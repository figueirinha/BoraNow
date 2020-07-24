using Recodme.RD.BoraNow.DataLayer.Users;
using System;
using System.ComponentModel.DataAnnotations;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Users
{
    public class CompanyViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Input a company name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Input a presentative name")]
        public string Representative { get; set; }
        [Required(ErrorMessage = "Input a phone number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Input a VAT Number")]
        public string VatNumber { get; set; }
        //public Guid ProfileId { get; set; }

        public Company ToCompany()
        {
            return new Company(Name,Representative, PhoneNumber, VatNumber/*, ProfileId*/);
        }

        public static CompanyViewModel Parse(Company company)
        {
            return new CompanyViewModel()
            {
                Id = company.Id,
                Name = company.Name,
                Representative = company.Representative,
                PhoneNumber = company.PhoneNumber,
                VatNumber = company.VatNumber
                //ProfileId = company.ProfileId
            };
        }
        public bool CompareToModel(Company model)
        {
            return Name == model.Name && Representative == model.Representative && PhoneNumber == model.PhoneNumber && VatNumber == model.VatNumber;
        }
    }
}
