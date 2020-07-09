using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.DataLayer.Quizzes;


namespace Recodme.RD.BoraNow.UnitTestProject.QuizzesTests
{
    public class OperationResult { public bool Success { get; set; } }

    [TestClass]
    public class CategoryQuizAnswerTests
    {
        [TestMethod]
        public void TestCreateCategoryQuizAnswer()
        {
            var _category = new Category("lets go tot eh beach each");
            var _quiz = new Quiz("Best Rapper in the World");
            var _quizQuestion = new QuizQuestion("nicki minaj ftw?", _quiz.Id);
            var _quizAnswer = new QuizAnswer("yesss", _quizQuestion.Id);
            var _categoryQuizAnswer = new CategoryQuizAnswer(_category.Id, _quizAnswer.Id);
            var _bo = new CategoryQuizAnswerBusinessObject();
            _bo.Create(_categoryQuizAnswer);
            var result = new OperationResult() { Success = true };
            var _categoryQuizAnswerCreated = _bo.Read(_categoryQuizAnswer.Id);
            Assert.IsTrue(_categoryQuizAnswerCreated.Result.QuizAnswerId == _categoryQuizAnswer.QuizAnswerId &&
                          _categoryQuizAnswerCreated.Result.CategoryId == _categoryQuizAnswer.CategoryId 
                          );
        }
        //public void TestDeleteCategoryQuizAnswerId()
        //{
        //    var _bo = new CategoryQuizAnswerBusinessObject();
        //    var _CategoryQuizAnswer = _bo.List().Result[0];
        //    var existingId = _CategoryQuizAnswer.Id;
        //    _bo.Delete(_CategoryQuizAnswer.Id);
        //    _CategoryQuizAnswer = _bo.List().Result[0];
        //    Assert.IsTrue(_CategoryQuizAnswer.Id == existingId);
        //}

    }
}
