using Recodme.RD.BoraNow.DataAccessLayer.Context;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recodme.RD.BoraNow.DataAccessLayer.Seeders
{
    public static class BoraNowSeeder
    {
        public static void Seed()
        {
            using var _ctx = new BoraNowContext();
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
            var category = new Category("VeganFood");
            var quiz = new Quiz("BoraNow quiz");
            var quizQuestion = new QuizQuestion("do you like food?", quiz.Id);
            var quizAnswer = new QuizAnswer("yes yes", quizQuestion.Id);
            var categoryQuizAnswer = new CategoryQuizAnswer(category.Id, quizAnswer.Id);
            var interestPoint = new InterestPoint("Abc", "very good food", "abc street", "C//uhuh", "3 am", "5 pm", "fridays", true, true);
            var interestPointCategory = new InterestPointCategory(interestPoint.Id, category.Id);
            var result = new Result("questionário nº 1", DateTime.Now, quiz.Id);
            var resultInterestPoint = new ResultInterestPoint(result.Id, interestPoint.Id);

            _ctx.Category.AddRange(category);
            _ctx.Quiz.AddRange(quiz);
            _ctx.QuizQuestion.AddRange(quizQuestion);
            _ctx.QuizAnswer.AddRange(quizAnswer);
            _ctx.CategoryQuizAnswer.AddRange(categoryQuizAnswer);
            _ctx.InterestPoint.AddRange(interestPoint);
            _ctx.InterestPointCategory.AddRange(interestPointCategory);
            _ctx.Result.AddRange(result);
            _ctx.ResultInterestPoint.AddRange(resultInterestPoint);
                      
            _ctx.SaveChanges();
        }
    }
}
