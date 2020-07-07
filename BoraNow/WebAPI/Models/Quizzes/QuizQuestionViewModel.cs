using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes
{
    public class QuizQuestionViewModel
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public Guid QuizId { get; set; }

        public QuizQuestion ToQuizQuestion()
        {
            return new QuizQuestion(Question, QuizId);
        }

        public static QuizQuestionViewModel Parse(QuizQuestion quizQuestion)
        {
            return new QuizQuestionViewModel()
            {
                Id = quizQuestion.Id,
                Question = quizQuestion.Question,
                QuizId = quizQuestion.QuizId
            };
        }
    }
}
