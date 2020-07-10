using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.DataAccessLayer.Seeders;
using Recodme.RD.BoraNow.DataLayer.Quizzes;

namespace Recodme.RD.BoraNow.UnitTestProject.Quizzes
{

    [TestClass]
    public class QuizTests
    {
        [TestMethod]
        public void TestCreateQuiz()
        {
            BoraNowSeeder.Seed();
            var _Quiz = new Quiz("Questionário BoraNow");
            var _bo = new QuizBusinessObject();
            var resCreate = _bo.Create(_Quiz);
            var resGet = _bo.Read(_Quiz.Id);
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }
        //[TestMethod]
        //public void TestUpdateQuiz()
        //{
        //    var newTitleQuiz = "Quiz Bora Now";
        //    var _bo = new QuizBusinessObject();
        //    var _Quiz = _bo.List().Result[0];
        //    _Quiz.Title = newTitleQuiz;
        //    _bo.Update(_Quiz);
        //    _Quiz = _bo.List().Result[0];
        //    Assert.IsTrue(_Quiz.Title == newTitleQuiz);
        //}
        //[TestMethod]
        //public void TestDeleteQuizId()
        //{
        //    var _bo = new QuizBusinessObject();
        //    var _Quiz = _bo.List().Result[0];
        //    var existingId = _Quiz.Id;
        //    _bo.Delete(_Quiz.Id);
        //    _Quiz = _bo.List().Result[0];
        //    Assert.IsTrue(_Quiz.Id == existingId);
        //}

    }
}
