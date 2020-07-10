using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
    public class ResultTests
    {
        [TestMethod]
        public void TestCreateResult()
        {
            BoraNowSeeder.Seed();
            var rbo = new ResultBusinessObject();
            var qbo = new QuizBusinessObject();

            var quiz = new Quiz("Quiz 1");
            qbo.Create(quiz);

            var _result = new Result("Q1 Result", DateTime.UtcNow, quiz.Id);

            var resCreate = rbo.Create(_result);
            var resGet = rbo.Read(_result.Id);
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        } 
        
        [TestMethod]
        public void TestListResult()
        {
            BoraNowSeeder.Seed();
            var bo = new ResultBusinessObject();
            var resList = bo.List();
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }
        
        [TestMethod]
        public void TestUpdateResult()
        {
            BoraNowSeeder.Seed();
            var rbo = new ResultBusinessObject();
            var resList = rbo.List();
            var item = resList.Result.FirstOrDefault();

            var qbo = new QuizBusinessObject();
            var quiz = new Quiz("Quiz 2");
            qbo.Create(quiz);

            item.QuizId = quiz.Id;
            var resUpdate = rbo.Update(item);
            resList = rbo.List();

            Assert.IsTrue(resList.Success && resUpdate.Success &&
                resList.Result.First().QuizId == item.QuizId);

        }

        [TestMethod]
        public void TestDeleteResultId()
        {
            BoraNowSeeder.Seed();
            var bo = new ResultBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            resList = bo.List();

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }

    }
}
