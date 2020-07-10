using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.DataLayer.Quizzes;

namespace UnitTestProject
{

    [TestClass]
    public class QuizTests
    {
        [TestMethod]
        public void TestCreateQuiz()
        {
            var _Quiz = new Quiz("Questionário BoraNow");
            var _bo = new QuizBusinessObject();
            _bo.Create(_Quiz);
            var _QuizCreated = _bo.Read(_Quiz.Id);
            Assert.IsTrue(_QuizCreated.Result.Title == _Quiz.Title);
        }
        [TestMethod]
        public void TestUpdateQuiz()
        {
            var newTitleQuiz = "Quiz Bora Now";
            var _bo = new QuizBusinessObject();
            var _Quiz = _bo.List().Result[0];
            _Quiz.Title = newTitleQuiz;
            _bo.Update(_Quiz);
            _Quiz = _bo.List().Result[0];
            Assert.IsTrue(_Quiz.Title == newTitleQuiz);
        }
        [TestMethod]
        public void TestDeleteQuizId()
        {
            var _bo = new QuizBusinessObject();
            var _Quiz = _bo.List().Result[0];
            var existingId = _Quiz.Id;
            _bo.Delete(_Quiz.Id);
            _Quiz = _bo.List().Result[0];
            Assert.IsTrue(_Quiz.Id == existingId);
        }

    }
}
