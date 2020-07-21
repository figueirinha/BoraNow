using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes
{
    public class InterestPointCategoryInterestPointViewModel
    {
        public Guid Id { get; set; }
        public Guid InterestPointId { get; set; }
        public Guid CategoryId { get; set; }


        public InterestPointCategoryInterestPoint ToInterestPointCategory()
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
