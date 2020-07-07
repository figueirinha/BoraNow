using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes
{
    public class InterestPointViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string PhotoPath { get; set; }
        public string OpeningHours { get; set; }
        public string ClosingHours { get; set; }
        public string ClosingDays { get; set; }
        public bool CovidSafe { get; set; }
        public bool Status { get; set; }

        public InterestPoint ToInterestPoint()
        {
            return new InterestPoint(Name, Description, Address, PhotoPath, OpeningHours, ClosingHours, ClosingDays, CovidSafe, Status);
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
                Status = interestPoint.Status
            };
        }
    }
}
