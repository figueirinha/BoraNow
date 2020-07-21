using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes
{
    public class CategoryInterestPointViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public CategoryInterestPoint ToCategoryInterestPoint()
        {
            return new CategoryInterestPoint(Name);
        }

        public static CategoryInterestPointViewModel Parse(CategoryInterestPoint categoryInterestPoint)
        {
            return new CategoryInterestPointViewModel()
            {
                Id = categoryInterestPoint.Id,
                Name = categoryInterestPoint.Name             
            };
        }
    }
}
