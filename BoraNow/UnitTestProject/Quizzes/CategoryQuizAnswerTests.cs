using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.DataAccessLayer.Seeders;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System.Linq;

namespace Recodme.RD.BoraNow.UnitTestProject.Quizzes
{
    [TestClass]
    public class CategoryQuizAnswerTests
    {
        [TestMethod]
        public void TestCreateCategoryQuizAnswer()
        {
            BoraNowSeeder.Seed();
            var cqbo = new CategoryQuizAnswerBusinessObject();
            var qbo = new QuizBusinessObject();
            var qqbo = new QuizQuestionBusinessObject();
            var qabo = new QuizAnswerBusinessObject();
            var cbo = new CategoryBusinessObject();
            
    
            var quiz = new Quiz("this quiz");
            var quizQuestion = new QuizQuestion("do u like food?", quiz.Id);
            var quizAnswer = new QuizAnswer("yes", quizQuestion.Id);
            var category = new Category("vegan");
            qbo.Create(quiz);
            qqbo.Create(quizQuestion);
            qabo.Create(quizAnswer);
            cbo.Create(category);
          
            var categoryQuiz = new CategoryQuizAnswer(category.Id, quizAnswer.Id);
            var resCreate = cqbo.Create(categoryQuiz);
            var resGet = cqbo.Read(categoryQuiz.Id);

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListCategoryQuizAnswer()
        {
            BoraNowSeeder.Seed();
            var bo = new CategoryQuizAnswerBusinessObject();
            var resList = bo.List();

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateCategoryQuizAnswer()
        {
            BoraNowSeeder.Seed();
            var cqbo = new CategoryQuizAnswerBusinessObject();
            var resList = cqbo.List();
            var item = resList.Result.FirstOrDefault();

            var qbo = new QuizBusinessObject();
            var qqbo = new QuizQuestionBusinessObject();
            var qabo = new QuizAnswerBusinessObject();
            var cbo = new CategoryBusinessObject();


            var quiz = new Quiz("this quiz");
            var quizQuestion = new QuizQuestion("do u like food?", quiz.Id);
            var quizAnswer = new QuizAnswer("yes", quizQuestion.Id);
            var category = new Category("vegan");
            qbo.Create(quiz);
            qqbo.Create(quizQuestion);
            qabo.Create(quizAnswer);
            cbo.Create(category);

            item.QuizAnswerId = quizAnswer.Id;
            item.CategoryId = category.Id;
            var resUpdate = cqbo.Update(item);
            resList = cqbo.List();

            Assert.IsTrue(resList.Success && resUpdate.Success &&
                resList.Result.First().CategoryId == item.CategoryId && resList.Result.First().QuizAnswerId == item.QuizAnswerId);
        }

        [TestMethod]
        public void TestDeleteCategoryQuizAnswer()
        {
            BoraNowSeeder.Seed();
            var bo = new CategoryQuizAnswerBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            resList = bo.List();

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }
    }
}
 