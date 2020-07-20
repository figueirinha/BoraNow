using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Newsletters;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.DataAccessLayer.Seeders;
using Recodme.RD.BoraNow.DataLayer.Newsletters;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recodme.RD.BoraNow.UnitTestProject.Newsletters
{
    [TestClass]
    public class InterestPointNewsletterTests
    {
        [TestMethod]
        public void TestCreateInterestPointNewsletter()
        {
            BoraNowSeeder.Seed();
            var ipnbo = new InterestPointNewsletterBusinessObject();

            var nbo = new NewsletterBusinessObject();
            var ipbo = new InterestPointBusinessObject();

            var news = new Newsletter("New in town, this doughnut place is nuts", "New in town");
            var interestPoint = new InterestPoint("a", "b", "c", "d", "e", "f", "g", true, true);
            nbo.Create(news);
            ipbo.Create(interestPoint);

            var interestPointNews = new InterestPointNewsletter(interestPoint.Id, news.Id);

            var resCreate = ipnbo.Create(interestPointNews);
            var restGet = ipnbo.Read(interestPointNews.Id);

            Assert.IsTrue(resCreate.Success && restGet.Success && restGet.Result != null);
        }

        [TestMethod]
        public void TestCreateInterestPointNewsletterAsync()
        {
            BoraNowSeeder.Seed();
            var ipnbo = new InterestPointNewsletterBusinessObject();

            var nbo = new NewsletterBusinessObject();
            var ipbo = new InterestPointBusinessObject();

            var news = new Newsletter("New in town, this doughnut place is nuts", "New in town");
            var interestPoint = new InterestPoint("a", "b", "c", "d", "e", "f", "g", true, true);
            nbo.Create(news);
            ipbo.Create(interestPoint);

            var interestPointNews = new InterestPointNewsletter(interestPoint.Id, news.Id);

            var resCreate = ipnbo.CreateAsync(interestPointNews).Result;
            var restGet = ipnbo.ReadAsync(interestPointNews.Id).Result;

            Assert.IsTrue(resCreate.Success && restGet.Success && restGet.Result != null);
        }

        [TestMethod]
        public void TestListInterestPointNewsletter()
        {
            BoraNowSeeder.Seed();
            var ipnbo = new InterestPointNewsletterBusinessObject();
            var resList = ipnbo.List();

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestListInterestPointNewsletterAsync()
        {
            BoraNowSeeder.Seed();
            var ipnbo = new InterestPointNewsletterBusinessObject();
            var resList = ipnbo.ListAsync().Result;

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateInterestPointNewsletter()
        {
            BoraNowSeeder.Seed();
            var ipnbo = new InterestPointNewsletterBusinessObject();
            var resList = ipnbo.List();
            var item = resList.Result.FirstOrDefault();

            var nbo = new NewsletterBusinessObject();
            var ipbo = new InterestPointBusinessObject();

            var news = new Newsletter("New in town, this doughnut place is nuts", "New in town");
            var interestPoint = new InterestPoint("a", "b", "c", "d", "e", "f", "g", true, true);
            nbo.Create(news);
            ipbo.Create(interestPoint);

            var newInterestPointNews = new InterestPointNewsletter(interestPoint.Id, news.Id);

            item.InterestPointId = newInterestPointNews.InterestPointId;
            item.NewsLetterId = newInterestPointNews.NewsLetterId;

            var resUpdate = ipnbo.Update(item);
            resList = ipnbo.List();

            Assert.IsTrue(resUpdate.Success && resList.Success &&
                resList.Result.First().InterestPointId == newInterestPointNews.InterestPointId && resList.Result.First().NewsLetterId == newInterestPointNews.NewsLetterId);
        }

        [TestMethod]
        public void TestUpdateInterestPointNewsletterAsync()
        {
            BoraNowSeeder.Seed();
            var ipnbo = new InterestPointNewsletterBusinessObject();
            var resList = ipnbo.List();
            var item = resList.Result.FirstOrDefault();

            var nbo = new NewsletterBusinessObject();
            var ipbo = new InterestPointBusinessObject();

            var news = new Newsletter("New in town, this doughnut place is nuts", "New in town");
            var interestPoint = new InterestPoint("a", "b", "c", "d", "e", "f", "g", true, true);
            nbo.Create(news);
            ipbo.Create(interestPoint);

            var newInterestPointNews = new InterestPointNewsletter(interestPoint.Id, news.Id);

            item.InterestPointId = newInterestPointNews.InterestPointId;
            item.NewsLetterId = newInterestPointNews.NewsLetterId;

            var resUpdate = ipnbo.UpdateAsync(item).Result;
            resList = ipnbo.ListAsync().Result;

            Assert.IsTrue(resUpdate.Success && resList.Success &&
                resList.Result.First().InterestPointId == newInterestPointNews.InterestPointId && resList.Result.First().NewsLetterId == newInterestPointNews.NewsLetterId);
        }

        [TestMethod]
        public void TestDeleteInterestPointNewsletter()
        {
            BoraNowSeeder.Seed();
            var ipnbo = new InterestPointNewsletterBusinessObject();
            var resList = ipnbo.List();
            var resDelete = ipnbo.Delete(resList.Result.First().Id);
            resList = ipnbo.List();

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }

        [TestMethod]
        public void TestDeleteInterestPointNewsletterAsync()
        {
            BoraNowSeeder.Seed();
            var ipnbo = new InterestPointNewsletterBusinessObject();
            var resList = ipnbo.List();
            var resDelete = ipnbo.DeleteAsync(resList.Result.First().Id).Result;
            resList = ipnbo.ListAsync().Result;

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }
    }
}
