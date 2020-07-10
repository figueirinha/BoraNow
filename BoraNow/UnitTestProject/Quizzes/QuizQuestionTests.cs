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

    public class QuizQuestionTests
    {
        [TestMethod]
        public void TestCreateQuizQuestion()
        {
            BoraNowSeeder.Seed();
            var qqbo = new QuizQuestionBusinessObject();
            var qbo = new QuizBusinessObject();

            var quiz = new Quiz("This quiz rocks");

            qbo.Create(quiz);

            var quizQuestion = new QuizQuestion("do u like wine?", quiz.Id);

            var resCreate = qqbo.Create(quizQuestion);
            var resGet = qqbo.Read(quizQuestion.Id);

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListQuizQuestion()
        {
            BoraNowSeeder.Seed();
            var bo = new QuizQuestionBusinessObject();
            var resList = bo.List();

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateQuizQuestion()
        {
            BoraNowSeeder.Seed();
            var qqbo = new QuizQuestionBusinessObject();
            var resList = qqbo.List();
            var item = resList.Result.FirstOrDefault();

            var qbo = new QuizBusinessObject();

            var quiz = new Quiz("This quiz rocks");

            qbo.Create(quiz);

            item.QuizId = quiz.Id;
            item.Question = "viewpoint?";
            

            var resUpdate = qqbo.Update(item);
            resList = qqbo.List();

            Assert.IsTrue(resUpdate.Success && resList.Success && resList.Result.First().Question == "viewpoint?"
                && resList.Result.First().QuizId == quiz.Id);
        }

        [TestMethod]
        public void TestDeleteQuizQuestion()
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
