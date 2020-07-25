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
            var categoryOne = new CategoryInterestPoint("VeganFood");
            var categoryTwo = new CategoryInterestPoint("SeaFood");
            var categoryThree = new CategoryInterestPoint("AsianFood");
            var quiz = new Quiz("BoraNow quiz");
            var quizQuestion = new QuizQuestion("What type of food are you looking for?", quiz.Id);
            var quizAnswerOne = new QuizAnswer("Vegan", quizQuestion.Id);
            var quizAnswerTwo = new QuizAnswer("Sea food", quizQuestion.Id);
            var quizAnswerThree = new QuizAnswer("Asian style", quizQuestion.Id);


            var countryOne = new Country("Angola");
            var countryTwo = new Country("Portugal");
            var profileOne = new Profile("blogueira vegana que adora viajar e conhecer novos locais", "mefamousstar.jpg");
            var profileTwo = new Profile("Business Man with a chain of restauarants across Lisbon", "merichstar.jpg");
            var company = new Company("PearTree Company", "Marco Pereria", "919200000", "23453554", profileTwo.Id);
            var visitor = new Visitor("Bruna", "Costa", DateTime.Now.AddYears(-24), "Female", profileOne.Id, countryOne.Id);


            var interestPoint = new InterestPoint("PearTree Abc", "very chill place that offers lots off tradicional food", "abc street", "uhuhuu.jpg", "3 am", "5 pm", "fridays", true, true, company.Id);
            var interestPointCategory = new InterestPointCategoryInterestPoint(interestPoint.Id, categoryOne.Id);
            var result = new Result("questionário nº 1", DateTime.Now, quiz.Id, visitor.Id);
            var resultInterestPoint = new ResultInterestPoint(result.Id, interestPoint.Id);

            var newsletter = new Newsletter("New place in town that has many vegan options", "Brand New");
            var interestPointNewsletter = new InterestPointNewsletter(interestPoint.Id, newsletter.Id);

            var meteorology = new Meteorology(19, 27, 0, 1, 0, DateTime.Now.AddDays(1));
            var feedback = new Feedback("very nice place, cousy vibes, really good food", 5, DateTime.Now.AddDays(-1), interestPoint.Id,visitor.Id);



            _ctx.Country.AddRange(countryOne);
            _ctx.Country.AddRange(countryTwo);
            _ctx.Profile.AddRange(profileOne);
            _ctx.Profile.AddRange(profileTwo);
            _ctx.Company.AddRange(company);
            _ctx.Visitor.AddRange(visitor);

            _ctx.Category.AddRange(categoryOne);
            _ctx.Category.AddRange(categoryTwo);
            _ctx.Category.AddRange(categoryThree);
            _ctx.Quiz.AddRange(quiz);
            _ctx.QuizQuestion.AddRange(quizQuestion);
            _ctx.QuizAnswer.AddRange(quizAnswerOne);
            _ctx.QuizAnswer.AddRange(quizAnswerTwo);
            _ctx.QuizAnswer.AddRange(quizAnswerThree);
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
