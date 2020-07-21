using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes
{
    public class CategoryInterestPointViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public CategoryInterestPoint ToCategory()
        {
            return new CategoryInterestPoint(Name);
        }

        public static CategoryInterestPointViewModel Parse(CategoryInterestPoint category)
        {
            return new CategoryInterestPointViewModel()
            {
                Id = category.Id,
                Name = category.Name             
            };
        }
    }
}
