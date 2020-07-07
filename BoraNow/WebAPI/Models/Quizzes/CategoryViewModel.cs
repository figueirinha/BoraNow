using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Category ToCategory()
        {
            return new Category(Name);
        }

        public static CategoryViewModel Parse(Category category)
        {
            return new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name             
            };
        }
    }
}
