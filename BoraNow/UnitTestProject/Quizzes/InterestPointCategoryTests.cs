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

    public class InterestPointCategoryTests
    {
        [TestMethod]
        public void TestCreateInterestPointCategory()
        {
            BoraNowSeeder.Seed();
            var ipcbo = new InterestPointCategoryInterestPointBusinessObject();
            var ipbo = new InterestPointBusinessObject();
            var cbo = new CategoryInterestPointBusinessObject();

            var interestPoint = new InterestPoint("a", "b", "c", "d", "e", "f", "g", true, true);
            var category = new CategoryInterestPoint("vegan");
            var interestPointCategory = new InterestPointCategoryInterestPoint(interestPoint.Id, category.Id);

            ipbo.Create(interestPoint);
            cbo.Create(category);

            var resCreate = ipcbo.Create(interestPointCategory);
            var resGet = ipcbo.Read(interestPointCategory.Id);

            Assert.IsTrue(resGet.Success && resCreate.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestCreateInterestPointCategoryAsync()
        {
            BoraNowSeeder.Seed();
            var ipcbo = new InterestPointCategoryInterestPointBusinessObject();
            var ipbo = new InterestPointBusinessObject();
            var cbo = new CategoryInterestPointBusinessObject();

            var interestPoint = new InterestPoint("a", "b", "c", "d", "e", "f", "g", true, true);
            var category = new CategoryInterestPoint("vegan");
            var interestPointCategory = new InterestPointCategoryInterestPoint(interestPoint.Id, category.Id);

            ipbo.Create(interestPoint);
            cbo.Create(category);

            var resCreate = ipcbo.CreateAsync(interestPointCategory).Result;
            var resGet = ipcbo.ReadAsync(interestPointCategory.Id).Result;

            Assert.IsTrue(resGet.Success && resCreate.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListInterestPointCategory()
        {
            BoraNowSeeder.Seed();
            var bo = new InterestPointCategoryInterestPointBusinessObject();
            var resList = bo.List();

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);

        }

        [TestMethod]
        public void TestListInterestPointCategoryAsync()
        {
            BoraNowSeeder.Seed();
            var bo = new InterestPointCategoryInterestPointBusinessObject();
            var resList = bo.ListAsync().Result;

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);

        }

        [TestMethod]
        public void TestUpdateInterestpointCategory()
        {
            BoraNowSeeder.Seed();
            var ipcbo = new InterestPointCategoryInterestPointBusinessObject();
            var resList = ipcbo.List();
            var item = resList.Result.FirstOrDefault();

            var ipbo = new InterestPointBusinessObject();
            var cbo = new CategoryInterestPointBusinessObject();

            var interestPoint = new InterestPoint("a", "b", "c", "d", "e", "f", "g", true, true);
            var category = new CategoryInterestPoint("vegan");

            ipbo.Create(interestPoint);
            cbo.Create(category);

            item.InterestPointId = interestPoint.Id;
            item.CategoryId = category.Id;
            var resUpdate = ipcbo.Update(item);
            resList = ipcbo.ListAsync().Result;

            Assert.IsTrue(resUpdate.Success && resList.Success && resList.Result.First().InterestPointId == interestPoint.Id
                && resList.Result.First().CategoryId == category.Id);

        }

        [TestMethod]
        public void TestUpdateInterestpointCategoryAsync()
        {
            BoraNowSeeder.Seed();
            var ipcbo = new InterestPointCategoryInterestPointBusinessObject();
            var resList = ipcbo.List();
            var item = resList.Result.FirstOrDefault();

            var ipbo = new InterestPointBusinessObject();
            var cbo = new CategoryInterestPointBusinessObject();

            var interestPoint = new InterestPoint("a", "b", "c", "d", "e", "f", "g", true, true);
            var category = new CategoryInterestPoint("vegan");

            ipbo.Create(interestPoint);
            cbo.Create(category);

            item.InterestPointId = interestPoint.Id;
            item.CategoryId = category.Id;
            var resUpdate = ipcbo.UpdateAsync(item).Result;
            resList = ipcbo.ListAsync().Result;

            Assert.IsTrue(resUpdate.Success && resList.Success && resList.Result.First().InterestPointId == interestPoint.Id
                && resList.Result.First().CategoryId == category.Id);

        }

        [TestMethod]
        public void TestDeleteInterestPointCategory()
        {
            BoraNowSeeder.Seed();
            var bo = new InterestPointCategoryInterestPointBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            resList = bo.List();

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);

        }


        [TestMethod]
        public void TestDeleteInterestPointCategoryAsync()
        {
            BoraNowSeeder.Seed();
            var bo = new InterestPointCategoryInterestPointBusinessObject();
            var resList = bo.List();
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListAsync().Result;

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);

        }
    }
}
