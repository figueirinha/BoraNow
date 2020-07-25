using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes
{
    public class ResultViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Input a title result")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Input a date")]
        public DateTime Date { get; set; }
        [Display(Name ="Quiz name")]
        public Guid QuizId { get; set; }
        [Display(Name = "Visitor's name")]
        public Guid VisitorId { get; set; }

        public string DateToString
        {
            get
            {
                return $"{Date.Day}-{Date.Month}-{Date.Year}";
            }
        }
        public Result ToResult()
        {
            return new Result(Title, Date, QuizId, VisitorId);
        }

        public static ResultViewModel Parse(Result result)
        {
            return new ResultViewModel()
            {
                Id = result.Id,
                Title = result.Title,
                Date = result.Date,
                QuizId = result.QuizId,
                VisitorId = result.VisitorId
            };
        }
        public bool CompareToModel(Result model)
        {
            return Title == model.Title && Date == model.Date && QuizId == model.QuizId && VisitorId == model.VisitorId;
        }
    }
}
