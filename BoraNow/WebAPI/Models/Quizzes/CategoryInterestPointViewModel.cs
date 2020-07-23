using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes
{
    public class CategoryInterestPointViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Input a category name")]
        [Display(Name = "Category name")]
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
        public bool CompareToModel(CategoryInterestPoint categoryInterestPoint)
        {
            return Name == categoryInterestPoint.Name;
        }

    }
}
