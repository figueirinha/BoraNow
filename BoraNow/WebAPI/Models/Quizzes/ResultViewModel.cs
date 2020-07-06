using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes
{
    public class ResultViewModel
    {
        public Guid Id { get; set; }
        public String Title { get; set; }
        public DateTime Date { get; set; }
        public Guid QuizId { get; set; }


        public Result ToResult()
        {
            return new Result(Title, Date, QuizId);
        }

        public static ResultViewModel Parse(Result result)
        {
            return new ResultViewModel()
            {
                Id = result.Id,
                Title = result.Title,
                Date = result.Date,
                QuizId = result.QuizId
            };
        }
    }
}
