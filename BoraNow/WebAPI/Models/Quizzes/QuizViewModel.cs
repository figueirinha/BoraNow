using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes
{
    public class QuizViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public Quiz ToQuiz()
        {
            return new Quiz(Title);
        }

        public static QuizViewModel Parse(Quiz quiz)
        {
            return new QuizViewModel()
            {
                Id = quiz.Id,
                Title = quiz.Title
            };
        }

       
    }
}
