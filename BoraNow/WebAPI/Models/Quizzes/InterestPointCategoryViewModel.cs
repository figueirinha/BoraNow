using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes
{
    public class InterestPointCategoryViewModel
    {
        public Guid Id { get; set; }
        public Guid InterestPointId { get; set; }
        public Guid CategoryId { get; set; }


        public InterestPointCategory ToInterestPointCategory()
        {
            return new InterestPointCategory(InterestPointId, CategoryId);
        }

        public static InterestPointCategoryViewModel Parse(InterestPointCategory interestPointCategory)
        {
            return new InterestPointCategoryViewModel()
            {
                Id = interestPointCategory.Id,
                InterestPointId = interestPointCategory.InterestPointId,
                CategoryId = interestPointCategory.CategoryId

            };
        }
    }
}
