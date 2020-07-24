using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes
{
    public class InterestPointCategoryInterestPointViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Interest Points")]
        public Guid InterestPointId { get; set; }
        [Display(Name = "Interest Points Categories")]
        public Guid CategoryId { get; set; }


        public InterestPointCategoryInterestPoint ToInterestPointCategoryInteresPoint()
        {
            return new InterestPointCategoryInterestPoint(InterestPointId, CategoryId);
        }

        public static InterestPointCategoryInterestPointViewModel Parse(InterestPointCategoryInterestPoint interestPointCategory)
        {
            return new InterestPointCategoryInterestPointViewModel()
            {
                Id = interestPointCategory.Id,
                InterestPointId = interestPointCategory.InterestPointId,
                CategoryId = interestPointCategory.CategoryId

            };
        }
    }
}
