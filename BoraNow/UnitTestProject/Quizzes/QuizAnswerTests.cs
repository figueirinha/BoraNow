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
            var qa = new QuizAnswerBusinessObject();
            var qq = new QuizQuestionBusinessObject();
            var quiz = new QuizBusinessObject();

            var newQuiz = new Quiz("Where you wanna go?");
            var newQuizQuestion = new QuizQuestion("Where you wanna go?", newQuiz.Id);
            var newQuizAnswer = new QuizAnswer("Beach", newQuizQuestion.Id);

            qq.Create(newQuizQuestion);
            quiz.Create(newQuiz);
            

            var resCreate = qa.Create(newQuizAnswer);
            var resGet = qa.Read(newQuizAnswer.Id);           

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
        public void TestUpdateQuizAnswer()
        {
            BoraNowSeeder.Seed();
            var qabo = new QuizAnswerBusinessObject();
            var qqbo = new QuizQuestionBusinessObject();
            var quizbo= new QuizBusinessObject();
            var resList = qabo.List();

            var newQuiz = new Quiz("Where you wanna go?");
            var newQuizQuestion = new QuizQuestion("Where you wanna go?", newQuiz.Id);
            var newQuizAnswer = new QuizAnswer("yes", newQuizQuestion.Id);
            var quizAnswer = resList.Result.FirstOrDefault();

            qqbo.Create(newQuizQuestion);
            quizbo.Create(newQuiz);
            quizAnswer.QuizQuestionId = newQuizAnswer.QuizQuestionId;
            quizAnswer.Answer = newQuizAnswer.Answer;

            var resUpdate= qabo.Update(quizAnswer);
            resList = qabo.List();

            Assert.IsTrue(resUpdate.Success && resList.Success && resList.Result.First().Answer == newQuizAnswer.Answer
                && resList.Result.First().QuizQuestionId == newQuizAnswer.QuizQuestionId);
        }

        [TestMethod]
        public void TestDeleteQuizAnswerId()
        {
            BoraNowSeeder.Seed();
            var bo = new QuizAnswerBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            resList = bo.List();

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }
    }
}
