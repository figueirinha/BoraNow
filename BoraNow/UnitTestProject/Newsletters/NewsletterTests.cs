using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Newsletters;
using Recodme.RD.BoraNow.DataAccessLayer.Seeders;
using Recodme.RD.BoraNow.DataLayer.Newsletters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recodme.RD.BoraNow.UnitTestProject.Newsletters
{
    [TestClass]
    public class NewsletterTests
    {
        [TestMethod]
        public void TestCreateNewsletter()
        {
            BoraNowSeeder.Seed();
            var nbo = new NewsletterBusinessObject();

            var news = new Newsletter("New in town, this doughnut place is nuts", "New in town");

            var resCreate = nbo.Create(news);
            var restGet = nbo.Read(news.Id);

            Assert.IsTrue(resCreate.Success && restGet.Success && restGet.Result != null);
        }

        [TestMethod]
        public void TestCreateNewsletterAsync()
        {
            BoraNowSeeder.Seed();
            var nbo = new NewsletterBusinessObject();

            var news = new Newsletter("New in town, this doughnut place is nuts", "New in town");

            var resCreate = nbo.CreateAsync(news).Result;
            var restGet = nbo.ReadAsync(news.Id).Result;

            Assert.IsTrue(resCreate.Success && restGet.Success && restGet.Result != null);
        }

        [TestMethod]
        public void TestListNewsletter()
        {
            BoraNowSeeder.Seed();
            var nbo = new NewsletterBusinessObject();
            var resList = nbo.List();

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestListNewsletterAsync()
        {
            BoraNowSeeder.Seed();
            var nbo = new NewsletterBusinessObject();
            var resList = nbo.ListAsync().Result;

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateNewsletter()
        {
            BoraNowSeeder.Seed();
            var nbo = new NewsletterBusinessObject();
            var resList = nbo.List();
            var item = resList.Result.FirstOrDefault();

            var newNews = new Newsletter("try it now, new burger down town", "Lisbon new burger place");

            item.Description = newNews.Description;
            item.Title = newNews.Title;            

            var resUpdate = nbo.Update(item);
            resList = nbo.List();

            Assert.IsTrue(resUpdate.Success && resList.Success &&
                resList.Result.First().Description == newNews.Description && resList.Result.First().Title == newNews.Title);
        }

        [TestMethod]
        public void TestUpdateNewsletterAsync()
        {
            BoraNowSeeder.Seed();
            var nbo = new NewsletterBusinessObject();
            var resList = nbo.List();
            var item = resList.Result.FirstOrDefault();

            var newNews = new Newsletter("try it now, new burger down town", "Lisbon new burger place");

            item.Description = newNews.Description;
            item.Title = newNews.Title;

            var resUpdate = nbo.UpdateAsync(item).Result;
            resList = nbo.ListAsync().Result;

            Assert.IsTrue(resUpdate.Success && resList.Success &&
                resList.Result.First().Description == newNews.Description && resList.Result.First().Title == newNews.Title);
        }

        [TestMethod]
        public void TestDeleteNewsletter()
        {
            BoraNowSeeder.Seed();
            var nbo = new NewsletterBusinessObject();
            var resList = nbo.List();
            var resDelete = nbo.Delete(resList.Result.First().Id);
            resList = nbo.List();

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }

        [TestMethod]
        public void TestDeleteNewsletterAsync()
        {
            BoraNowSeeder.Seed();
            var nbo = new NewsletterBusinessObject();
            var resList = nbo.List();
            var resDelete = nbo.DeleteAsync(resList.Result.First().Id).Result;
            resList = nbo.ListAsync().Result;

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }
    }
}
