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

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Address { get; set; }

        [Display(Name = "Introduce a picture")]
        [Required]
        public string PhotoPath { get; set; }

        [Display(Name = "Opening Hours")]
        [Required]
        public string OpeningHours { get; set; }

        [Display(Name = "Closing Hours")]
        [Required]
        public string ClosingHours { get; set; }

        [Display(Name = "Closing Days")]
        public string ClosingDays { get; set; }

        [Display(Name = "Covid-19 Safe")]
        [Required]
        public bool CovidSafe { get; set; }

        [Required]
        public bool Status { get; set; }
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
