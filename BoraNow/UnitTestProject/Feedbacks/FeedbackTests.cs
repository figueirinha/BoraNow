using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Feedbacks;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Users;
using Recodme.RD.BoraNow.DataAccessLayer.Seeders;
using Recodme.RD.BoraNow.DataLayer.Feedbacks;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using Recodme.RD.BoraNow.DataLayer.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recodme.RD.BoraNow.UnitTestProject.Feedbacks
{
    [TestClass]
    public class FeedbackTests
    {
        [TestMethod]
        public void TestCreateFeedback()
        {
            BoraNowSeeder.Seed();
            var fbo = new FeedbackBusinessObject();
            var pbo = new ProfileBusinessObject();
            var cbo = new CompanyBusinessObject();
            var ipbo = new InterestPointBusinessObject();

            var profile = new Profile("a","b",Guid.NewGuid(),)
            var company = new Company("a","b","c","d",);
            var interestpoint = new InterestPoint("a", "b", "c", "d", "e", "f", "g", true, true);
            ipbo.Create(interestpoint);

            var feedback = new Feedback("good", 3, DateTime.Now, interestpoint.Id);

            var resCreate = fbo.Create(feedback);
            var resGet = fbo.Read(feedback.Id);

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestCreateFeedbackAsync()
        {
            BoraNowSeeder.Seed();
            var fbo = new FeedbackBusinessObject();
            var ipbo = new InterestPointBusinessObject();

            var interestpoint = new InterestPoint("a", "b", "c", "d", "e", "f", "g", true, true);
            ipbo.Create(interestpoint);

            var feedback = new Feedback("good", 3, DateTime.Now, interestpoint.Id);

            var resCreate = fbo.CreateAsync(feedback).Result;
            var resGet = fbo.ReadAsync(feedback.Id).Result;

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListFeedback()
        {
            BoraNowSeeder.Seed();
            var bo = new FeedbackBusinessObject();
            var resList = bo.List();

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestListFeedbackAsync()
        {
            BoraNowSeeder.Seed();
            var bo = new FeedbackBusinessObject();
            var resList = bo.ListAsync().Result;

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateFeedback()
        {
            BoraNowSeeder.Seed();
            var fbo = new  FeedbackBusinessObject();
            var resList = fbo.List();
            var item = resList.Result.FirstOrDefault();

            var ipbo = new InterestPointBusinessObject();

            var interestpoint = new InterestPoint("a", "b", "c", "d", "e", "f", "g", true, true);
            ipbo.Create(interestpoint);

            var feedback = new Feedback("good", 3, DateTime.Now, interestpoint.Id);

            item.Description = feedback.Description;
            item.Stars = feedback.Stars;
            item.Date = feedback.Date;
            item.InterestPointId = feedback.InterestPointId;

            var resUpdate = fbo.Update(item);
            resList = fbo.List();

            Assert.IsTrue(resList.Success && resUpdate.Success &&
                resList.Result.First().Description == feedback.Description && resList.Result.First().Stars == feedback.Stars
                && resList.Result.First().Date == feedback.Date && resList.Result.First().InterestPointId == feedback.InterestPointId);
        }

        [TestMethod]
        public void TestUpdateFeedbackAsync()
        {
            BoraNowSeeder.Seed();
            var fbo = new FeedbackBusinessObject();
            var resList = fbo.List();
            var item = resList.Result.FirstOrDefault();

            var ipbo = new InterestPointBusinessObject();

            var interestpoint = new InterestPoint("a", "b", "c", "d", "e", "f", "g", true, true);
            ipbo.Create(interestpoint);

            var feedback = new Feedback("good", 3, DateTime.Now, interestpoint.Id);

            item.Description = feedback.Description;
            item.Stars = feedback.Stars;
            item.Date = feedback.Date;
            item.InterestPointId = feedback.InterestPointId;

            var resUpdate = fbo.UpdateAsync(item).Result;
            resList = fbo.ListAsync().Result;

            Assert.IsTrue(resList.Success && resUpdate.Success &&
                resList.Result.First().Description == feedback.Description && resList.Result.First().Stars == feedback.Stars
                && resList.Result.First().Date == feedback.Date && resList.Result.First().InterestPointId == feedback.InterestPointId);
        }

        [TestMethod]
        public void TestDeleteFeedback()
        {
            BoraNowSeeder.Seed();
            var bo = new FeedbackBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            resList = bo.List();

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }


        [TestMethod]
        public void TestDeleteFeedbackAsync()
        {
            BoraNowSeeder.Seed();
            var bo = new FeedbackBusinessObject();
            var resList = bo.List();
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListAsync().Result;

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }
    }
}
