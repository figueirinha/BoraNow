using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes
{
    public class CategoryQuizAnswerViewModel
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid QuizAnswerId { get; set; }

        public CategoryQuizAnswer ToCategoryQuizAnswer()
        {
            return new CategoryQuizAnswer(CategoryId, QuizAnswerId);
        }

        public static CategoryQuizAnswerViewModel Parse(CategoryQuizAnswer categoryQuizAnswer)
        {
            return new CategoryQuizAnswerViewModel()
            {
                Id = categoryQuizAnswer.Id,
                CategoryId = categoryQuizAnswer.CategoryId,
                QuizAnswerId = categoryQuizAnswer.QuizAnswerId
            };
        }
    }
}
