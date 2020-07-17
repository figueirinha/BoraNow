using Recodme.RD.BoraNow.DataLayer.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Users
{
    public class CountryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
      
        public Country ToCountry()
        {
            return new Country(Name);
        }

        public static CountryViewModel Parse(Country country)
        {
            return new CountryViewModel()
            {
                Id = country.Id,
                Name = country.Name
               
            };
        }
    }
}
