using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.DataAccessLayer.Seeders;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System.Linq;


namespace Recodme.RD.BoraNow.UnitTestProject.Quizzes
{
    [TestClass]
    public class CategoryInterestPointTests
    {
        [TestMethod]
        public void TestCreateCategoryInterestPoint()
        {
            BoraNowSeeder.Seed();
            var cipbo = new CategoryInterestPointBusinessObject();

            var categoryInterestPoint= new CategoryInterestPoint("B");

            var resCreate = cipbo.Create(categoryInterestPoint);
            var resGet = cipbo.Read(categoryInterestPoint.Id);

            Assert.IsTrue(resGet.Success && resCreate.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestCreateCategoryInterestPointAsync()
        {
            BoraNowSeeder.Seed();
            var cipbo = new CategoryInterestPointBusinessObject();

            var categoryInterestPoint = new CategoryInterestPoint("B");
 

            var resCreate = cipbo.CreateAsync(categoryInterestPoint).Result;
            var resGet = cipbo.ReadAsync(categoryInterestPoint.Id).Result;

            Assert.IsTrue(resGet.Success && resCreate.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListCategoryInterestPoint()
        {
            BoraNowSeeder.Seed();
            var cipbo = new CategoryInterestPointBusinessObject();
            var resList = cipbo.List();

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);

        }

        [TestMethod]
        public void TestListCategoryInterestPointAsync()
        {
            BoraNowSeeder.Seed();
            var cipbo = new CategoryInterestPointBusinessObject();
            var resList = cipbo.ListAsync().Result;

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);

        }

        [TestMethod]
        public void TestUpdatetCategoryInterestPoint()
        {
            BoraNowSeeder.Seed();
            var cipbo = new CategoryInterestPointBusinessObject();
            var resList = cipbo.List();
            var item = resList.Result.FirstOrDefault();

   
            var categoryInterestPoint = new CategoryInterestPoint("C");

            item.Name = categoryInterestPoint.Name;
            var resUpdate = cipbo.Update(item);
            resList = cipbo.List();

            Assert.IsTrue(resUpdate.Success && resList.Success && resList.Result.First().Name == categoryInterestPoint.Name);

        }

        [TestMethod]
        public void TestUpdateInterestpointCategoryAsync()
        {
            BoraNowSeeder.Seed();
            var cipbo = new CategoryInterestPointBusinessObject();
            var resList = cipbo.List();
            var item = resList.Result.FirstOrDefault();

            var categoryInterestPoint = new CategoryInterestPoint("C");

            item.Name = categoryInterestPoint.Name;
            var resUpdate = cipbo.UpdateAsync(item).Result;
            resList = cipbo.ListAsync().Result;

            Assert.IsTrue(resUpdate.Success && resList.Success && resList.Result.First().Name == categoryInterestPoint.Name);
        }

        [TestMethod]
        public void TestDeleteInterestPointCategory()
        {
            BoraNowSeeder.Seed();
            var bo = new CategoryInterestPointBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            resList = bo.List();

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);

        }


        [TestMethod]
        public void TestDeleteInterestPointCategoryAsync()
        {
            BoraNowSeeder.Seed();
            var bo = new CategoryInterestPointBusinessObject();
            var resList = bo.List();
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListAsync().Result;

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }
    }
}
