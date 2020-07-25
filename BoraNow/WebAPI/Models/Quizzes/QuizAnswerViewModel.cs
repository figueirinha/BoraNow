using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.ComponentModel.DataAnnotations;


namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes
{
    public class QuizAnswerViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Please insert an Answer")]
        public string Answer { get; set; }

        [Required(ErrorMessage = "Please insert a Question")]
        [Display(Name = "Question")]
        public Guid QuizQuestionId { get; set; }

        public QuizAnswer ToQuizAnswer()
        {
            return new QuizAnswer(Answer, QuizQuestionId);
        }

        public static QuizAnswerViewModel Parse(QuizAnswer quizAnswer)
        {
            return new QuizAnswerViewModel()
            {
                Id = quizAnswer.Id,
                Answer = quizAnswer.Answer,
                QuizQuestionId = quizAnswer.QuizQuestionId
            };
        }
    }
}
