using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.DataAccessLayer.Seeders;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System.Linq;

namespace Recodme.RD.BoraNow.UnitTestProject.Quizzes
{

    [TestClass]
    public class QuizTests
    {
        [TestMethod]
        public void TestCreateQuiz()
        {
            BoraNowSeeder.Seed();
            var _quiz = new Quiz("Questionário BoraNow");
            var _bo = new QuizBusinessObject();
            var resCreate = _bo.Create(_quiz);
            var resGet = _bo.Read(_quiz.Id);

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestCreateQuizAsync()
        {
            BoraNowSeeder.Seed();
            var _quiz = new Quiz("Questionário BoraNow");
            var _bo = new QuizBusinessObject();
            var resCreate = _bo.CreateAsync(_quiz).Result;
            var resGet = _bo.ReadAsync(_quiz.Id).Result;

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListQuiz()
        {
            BoraNowSeeder.Seed();
            var bo = new QuizBusinessObject();
            var resList = bo.List();

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestListQuizAsync()
        {
            BoraNowSeeder.Seed();
            var bo = new QuizBusinessObject();
            var resList = bo.ListAsync().Result;

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateQuiz()
        {
            BoraNowSeeder.Seed();
            var qbo = new QuizBusinessObject();
            var resList = qbo.List();

            var quiz = resList.Result.FirstOrDefault();
            var newQuiz = new Quiz("BoraNow Quiz");

            quiz.Title = newQuiz.Title;

            var resUpdate = qbo.Update(quiz);
            resList = qbo.List();
            Assert.IsTrue(resUpdate.Success && resList.Success && resList.Result.First().Title == quiz.Title);
        }

        [TestMethod]
        public void TestUpdateQuizAsync()
        {
            BoraNowSeeder.Seed();
            var qbo = new QuizBusinessObject();
            var resList = qbo.List();

            var quiz = resList.Result.FirstOrDefault();
            var newQuiz = new Quiz("BoraNow Quiz");

            quiz.Title = newQuiz.Title;

            var resUpdate = qbo.UpdateAsync(quiz).Result;
            resList = qbo.ListAsync().Result;
            Assert.IsTrue(resUpdate.Success && resList.Success && resList.Result.First().Title == quiz.Title);
        }

        [TestMethod]
        public void TesDeletetQuiz()
        {
            BoraNowSeeder.Seed();
            var bo = new QuizBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            resList = bo.List();

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);

        }

        [TestMethod]
        public void TesDeletetQuizAsync()
        {
            BoraNowSeeder.Seed();
            var bo = new QuizBusinessObject();
            var resList = bo.List();
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListAsync().Result;

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);

        }
    }
}
