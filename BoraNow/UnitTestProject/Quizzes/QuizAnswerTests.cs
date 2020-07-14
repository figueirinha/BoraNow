using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.DataAccessLayer.Seeders;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recodme.RD.BoraNow.UnitTestProject.Quizzes
{
    [TestClass]

    public class QuizAnswerTests
    {
        [TestMethod]
        public void TestCreateQuizAnswer()
        {
            BoraNowSeeder.Seed();
            var qabo = new QuizAnswerBusinessObject();
            var qqbo = new QuizQuestionBusinessObject();
            var quizbo = new QuizBusinessObject();

            var newQuiz = new Quiz("Where you wanna go?");
            var newQuizQuestion = new QuizQuestion("Where you wanna go?", newQuiz.Id);

            quizbo.Create(newQuiz);
            qqbo.Create(newQuizQuestion);

            var newQuizAnswer = new QuizAnswer("Beach", newQuizQuestion.Id);

            var resCreate = qabo.Create(newQuizAnswer);
            var resGet = qabo.Read(newQuizAnswer.Id);           

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestCreateQuizAnswerAsync()
        {
            BoraNowSeeder.Seed();
            var qabo = new QuizAnswerBusinessObject();
            var qqbo = new QuizQuestionBusinessObject();
            var quizbo = new QuizBusinessObject();

            var newQuiz = new Quiz("Where you wanna go?");
            var newQuizQuestion = new QuizQuestion("Where you wanna go?", newQuiz.Id);

            quizbo.Create(newQuiz);
            qqbo.Create(newQuizQuestion);

            var newQuizAnswer = new QuizAnswer("Beach", newQuizQuestion.Id);

            var resCreate = qabo.CreateAsync(newQuizAnswer).Result;
            var resGet = qabo.ReadAsync(newQuizAnswer.Id).Result;

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListQuizAnswer()
        {
            BoraNowSeeder.Seed();
            var bo = new QuizAnswerBusinessObject();
            var resList = bo.List();

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestListQuizAnswerAsync()
        {
            BoraNowSeeder.Seed();
            var bo = new QuizAnswerBusinessObject();
            var resList = bo.ListAsync().Result;

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateQuizAnswer()
        {
            BoraNowSeeder.Seed();
            var qabo = new QuizAnswerBusinessObject();
            var qqbo = new QuizQuestionBusinessObject();
            var quizbo= new QuizBusinessObject();
            var resList = qabo.List();
            var quizAnswer = resList.Result.FirstOrDefault();


            var newQuiz = new Quiz("Where you wanna go?");
            var newQuizQuestion = new QuizQuestion("Where you wanna go?", newQuiz.Id);

            quizbo.Create(newQuiz);
            qqbo.Create(newQuizQuestion);

            quizAnswer.QuizQuestionId = newQuizQuestion.Id;
            quizAnswer.Answer = "yes";

            var resUpdate= qabo.Update(quizAnswer);
            resList = qabo.List();

            Assert.IsTrue(resUpdate.Success && resList.Success && resList.Result.First().Answer == "yes"
                && resList.Result.First().QuizQuestionId == newQuizQuestion.Id);
        }

        [TestMethod]
        public void TestUpdateQuizAnswerAsync()
        {
            BoraNowSeeder.Seed();
            var qabo = new QuizAnswerBusinessObject();
            var qqbo = new QuizQuestionBusinessObject();
            var quizbo = new QuizBusinessObject();
            var resList = qabo.List();
            var quizAnswer = resList.Result.FirstOrDefault();


            var newQuiz = new Quiz("Where you wanna go?");
            var newQuizQuestion = new QuizQuestion("Where you wanna go?", newQuiz.Id);

            quizbo.Create(newQuiz);
            qqbo.Create(newQuizQuestion);

            quizAnswer.QuizQuestionId = newQuizQuestion.Id;
            quizAnswer.Answer = "yes";

            var resUpdate = qabo.UpdateAsync(quizAnswer).Result;
            resList = qabo.ListAsync().Result;

            Assert.IsTrue(resUpdate.Success && resList.Success && resList.Result.First().Answer == "yes"
                && resList.Result.First().QuizQuestionId == newQuizQuestion.Id);
        }

        [TestMethod]
        public void TestDeleteQuizAnswer()
        {
            BoraNowSeeder.Seed();
            var bo = new QuizAnswerBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            resList = bo.List();

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }


        [TestMethod]
        public void TestDeleteQuizAnswerAsync()
        {
            BoraNowSeeder.Seed();
            var bo = new QuizAnswerBusinessObject();
            var resList = bo.List();
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListAsync().Result;

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }
    }
}
