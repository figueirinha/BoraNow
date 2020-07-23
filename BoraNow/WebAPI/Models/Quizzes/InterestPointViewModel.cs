using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes
{
    public class InterestPointViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Insert a name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Insert a description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Insert an address")]
        public string Address { get; set; }

        [Display(Name = "Introduce a picture")]
        public string PhotoPath { get; set; }

        [Display(Name = "Opening Hours")]
        [Required(ErrorMessage = "Insert the opening hours")]
        public string OpeningHours { get; set; }

        [Display(Name = "Closing Hours")]
        [Required(ErrorMessage = "Insert the cloing hours")]
        public string ClosingHours { get; set; }

        [Display(Name = "Closing Days")]
        public string ClosingDays { get; set; }

        [Display(Name = "Covid-19 Safe")]
        public bool CovidSafe { get; set; }
        public bool Status { get; set; }

        [Required(ErrorMessage = "What's the company company")]
        public Guid CompanyId { get; set; }

        public InterestPoint ToInterestPoint()
        {
            return new InterestPoint(Name, Description, Address, PhotoPath, OpeningHours, ClosingHours, ClosingDays, CovidSafe, Status, CompanyId);
        }

        public static InterestPointViewModel Parse(InterestPoint interestPoint)
        {
            return new InterestPointViewModel()
            {
                Id = interestPoint.Id,
                Name = interestPoint.Name,
                Description = interestPoint.Description,
                Address = interestPoint.Address,
                PhotoPath = interestPoint.PhotoPath,
                OpeningHours = interestPoint.OpeningHours,
                ClosingHours = interestPoint.ClosingHours,
                ClosingDays = interestPoint.ClosingDays,
                CovidSafe = interestPoint.CovidSafe,
                Status = interestPoint.Status,
                CompanyId = interestPoint.CompanyId
            };
        }
    }
}
