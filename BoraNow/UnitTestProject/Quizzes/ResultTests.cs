using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Users;
using Recodme.RD.BoraNow.DataAccessLayer.Seeders;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using Recodme.RD.BoraNow.DataLayer.Users;
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
            var vbo = new VisitorBusinessObject();

            var quiz = new Quiz("Quiz 1");
            qbo.Create(quiz);

            var visitor = new Visitor("B", "N", DateTime.Now,"Male");
            vbo.Create(visitor);


            var result = new Result("Q1 Result", DateTime.UtcNow, quiz.Id, visitor.Id);

            var resCreate = rbo.Create(result);
            var resGet = rbo.Read(result.Id);
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestCreateResultAsync()
        {
            var rbo = new ResultBusinessObject();
            var qbo = new QuizBusinessObject();
            var vbo = new VisitorBusinessObject();

            var quiz = new Quiz("Quiz 1");
            qbo.Create(quiz);

            var visitor = new Visitor("B", "N", DateTime.Now, "Male");
            vbo.Create(visitor);

            var result = new Result("Q1 Result", DateTime.UtcNow, quiz.Id, visitor.Id);

            var resCreate = rbo.CreateAsync(result).Result;
            var resGet = rbo.ReadAsync(result.Id).Result;
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
        public void TestListResultAsync()
        {
            BoraNowSeeder.Seed();
            var bo = new ResultBusinessObject();
            var resList = bo.ListAsync().Result;
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

            var vbo = new VisitorBusinessObject();
            var visitor = new Visitor("B", "N", DateTime.Now, "Male");
            vbo.Create(visitor);

            var result = new Result("Q1 Result", DateTime.UtcNow, quiz.Id, visitor.Id);

            item.Title = result.Title;
            item.Date = result.Date;
            item.QuizId = result.QuizId;
            item.VisitorId = result.VisitorId;
            var resUpdate = rbo.Update(item);
            resList = rbo.List();

            Assert.IsTrue(resList.Success && resUpdate.Success &&
                resList.Result.First().Title == item.Title &&
                resList.Result.First().Date == item.Date &&
                resList.Result.First().QuizId == item.QuizId &&
                resList.Result.First().VisitorId == item.VisitorId );
        }

        [TestMethod]
        public void TestUpdateResultAsync()
        {
            BoraNowSeeder.Seed();
            var rbo = new ResultBusinessObject();
            var resList = rbo.List();
            var item = resList.Result.FirstOrDefault();

            var qbo = new QuizBusinessObject();
            var quiz = new Quiz("Quiz 2");
            qbo.Create(quiz);

            var vbo = new VisitorBusinessObject();
            var visitor = new Visitor("B", "N", DateTime.Now, "Male");
            vbo.Create(visitor);

            var result = new Result("Q1 Result", DateTime.UtcNow, quiz.Id, visitor.Id);

            item.Title = result.Title;
            item.Date = result.Date;
            item.QuizId = result.QuizId;
            item.VisitorId = result.VisitorId;
            var resUpdate = rbo.Update(item);
            resList = rbo.ListAsync().Result;

            Assert.IsTrue(resList.Success && resUpdate.Success &&
                resList.Result.First().Title == item.Title &&
                resList.Result.First().Date == item.Date &&
                resList.Result.First().QuizId == item.QuizId &&
                resList.Result.First().VisitorId == item.VisitorId);

        }

        [TestMethod]
        public void TestDeleteResult()
        {
            BoraNowSeeder.Seed();
            var bo = new ResultBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            resList = bo.List();

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }

        [TestMethod]
        public void TestDeleteResultAsync()
        {
            BoraNowSeeder.Seed();
            var bo = new ResultBusinessObject();
            var resList = bo.List();
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListAsync().Result;

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }

    }
}
