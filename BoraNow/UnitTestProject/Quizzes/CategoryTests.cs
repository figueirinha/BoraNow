using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.DataAccessLayer.Seeders;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System.Linq;


namespace Recodme.RD.BoraNow.UnitTestProject.Quizzes
{

    [TestClass]
    public class CategoryTests
    {
        [TestMethod]
        public void TestCreateCategory()
        {
            BoraNowSeeder.Seed();
            var cbo = new CategoryBusinessObject();

            var category = new Category("Beach");

            var resCreate = cbo.Create(category);
            var resGet = cbo.Read(category.Id);
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestCreateCategoryAsync()
        {
            BoraNowSeeder.Seed();
            var cbo = new CategoryBusinessObject();

            var category = new Category("Beach");

            var resCreate = cbo.CreateAsync(category).Result;
            var resGet = cbo.ReadAsync(category.Id).Result;
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListCategory()
        {
            BoraNowSeeder.Seed();
            var bo = new CategoryBusinessObject();
            var resList = bo.List();

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestListCategoryAsync()
        {
            BoraNowSeeder.Seed();
            var bo = new CategoryBusinessObject();
            var resList = bo.ListAsync().Result;

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateCategory()
        {
            BoraNowSeeder.Seed();
            var bo = new CategoryBusinessObject();
            var resList = bo.List();

            var category = resList.Result.FirstOrDefault();
            category.Name = "Bar";
      
            var resUpdate = bo.Update(category);
            resList = bo.List();
            Assert.IsTrue(resUpdate.Success && resList.Success && resList.Result.First().Name == category.Name);
        }

        [TestMethod]
        public void TestUpdateCategoryAsync()
        {
            BoraNowSeeder.Seed();
            var bo = new CategoryBusinessObject();
            var resList = bo.List();

            var category = resList.Result.FirstOrDefault();
            category.Name = "Bar";

            var resUpdate = bo.UpdateAsync(category).Result;
            resList = bo.ListAsync().Result;
            Assert.IsTrue(resUpdate.Success && resList.Success && resList.Result.First().Name == category.Name);
        }

        [TestMethod]
        public void TestDeleteCategory()
        {
            BoraNowSeeder.Seed();
            var bo = new CategoryBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            resList = bo.List();

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }

        [TestMethod]
        public void TestDeleteCategoryAsync()
        {
            BoraNowSeeder.Seed();
            var bo = new CategoryBusinessObject();
            var resList = bo.List();
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListAsync().Result;

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }

    }
}
