using Recodme.RD.BoraNow.DataAccessLayer.Context;
using Recodme.RD.BoraNow.DataLayer.Feedbacks;
using Recodme.RD.BoraNow.DataLayer.Meteo;
using Recodme.RD.BoraNow.DataLayer.Newsletters;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using Recodme.RD.BoraNow.DataLayer.Users;
using System;


namespace Recodme.RD.BoraNow.DataAccessLayer.Seeders
{
    public static class BoraNowSeeder
    {
        public static void  Seed()
        {
            using var _ctx = new BoraNowContext();
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
            var category = new Category("VeganFood");
            var quiz = new Quiz("BoraNow quiz");
            var quizQuestion = new QuizQuestion("do you like food?", quiz.Id);
            var quizAnswer = new QuizAnswer("yes yes", quizQuestion.Id);
            var categoryQuizAnswer = new CategoryQuizAnswer(category.Id, quizAnswer.Id);

           
            var country = new Country("AA");
            var profile = new Profile("asdf", "ser", country.Id);
            var company = new Company("A", "BS", "12453546", "23453554", profile.Id);
            var visitor = new Visitor("BB", "CC", DateTime.Now, "EE", profile.Id);


            var interestPoint = new InterestPoint("Abc", "very good food", "abc street", "C//uhuh", "3 am", "5 pm", "fridays", true, true, company.Id);
            var interestPointCategory = new InterestPointCategory(interestPoint.Id, category.Id);
            var result = new Result("questionário nº 1", DateTime.Now, quiz.Id, visitor.Id);
            var resultInterestPoint = new ResultInterestPoint(result.Id, interestPoint.Id);

            var newsletter = new Newsletter("AAA", "abc");
            var interestPointNewsletter = new InterestPointNewsletter(interestPoint.Id, newsletter.Id);

            var meteorology = new Meteorology(19, 27, 0, 1, 0, DateTime.Now.AddDays(1));
            var feedback = new Feedback("very nice yes yes", 5, DateTime.Now.AddDays(-1), interestPoint.Id, profile.Id);



            _ctx.Country.AddRange(country);
            _ctx.Profile.AddRange(profile);
            _ctx.Company.AddRange(company);
            _ctx.Visitor.AddRange(visitor);

            _ctx.Category.AddRange(category);
            _ctx.Quiz.AddRange(quiz);
            _ctx.QuizQuestion.AddRange(quizQuestion);
            _ctx.QuizAnswer.AddRange(quizAnswer);
            _ctx.CategoryQuizAnswer.AddRange(categoryQuizAnswer);
            _ctx.InterestPoint.AddRange(interestPoint);
            _ctx.InterestPointCategory.AddRange(interestPointCategory);
            _ctx.Result.AddRange(result);
            _ctx.ResultInterestPoint.AddRange(resultInterestPoint);

            _ctx.Newsletter.AddRange(newsletter);
            _ctx.InterestPointNewsletter.AddRange(interestPointNewsletter);

            _ctx.Feedback.AddRange(feedback);
            _ctx.Meteorology.AddRange(meteorology);


            _ctx.SaveChanges();
        }
    }
}
