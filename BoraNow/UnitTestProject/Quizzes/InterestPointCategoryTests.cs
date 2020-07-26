using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Users;
using Recodme.RD.BoraNow.DataAccessLayer.Seeders;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using Recodme.RD.BoraNow.DataLayer.Users;
using System.Linq;


namespace Recodme.RD.BoraNow.UnitTestProject.Quizzes
{
    [TestClass]

    public class InterestPointCategoryInterestPointTests
    {
        [TestMethod]
        public void TestCreateInterestPointCategory()
        {
            BoraNowSeeder.Seed();
            var ipcipbo = new InterestPointCategoryInterestPointBusinessObject();
            var ipbo = new InterestPointBusinessObject();
            var cipbo = new CategoryInterestPointBusinessObject();
            var pbo = new ProfileBusinessObject();

            var profile = new Profile("II", "AA");
            pbo.Create(profile);

            var c = new CompanyBusinessObject();
            var company = new Company("A", "B", "12345678", "1234567", profile.Id);
            c.Create(company);

            var interestPoint = new InterestPoint("a", "b", "c", "d", "e", "f", "g", true, true, company.Id);
            var category = new CategoryInterestPoint("vegan");
            var interestPointCategory = new InterestPointCategoryInterestPoint(interestPoint.Id, category.Id);

            ipbo.Create(interestPoint);
            cipbo.Create(category);

            var resCreate = ipcipbo.Create(interestPointCategory);
            var resGet = ipcipbo.Read(interestPointCategory.Id);

            Assert.IsTrue(resGet.Success && resCreate.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestCreateInterestPointCategoryAsync()
        {
            BoraNowSeeder.Seed();
            var ipcipbo = new InterestPointCategoryInterestPointBusinessObject();
            var ipbo = new InterestPointBusinessObject();
            var cipbo = new CategoryInterestPointBusinessObject();
            var pbo = new ProfileBusinessObject();

            var profile = new Profile("II", "AA");
            pbo.Create(profile);

            var c = new CompanyBusinessObject();
            var company = new Company("A", "B", "12345678", "1234567", profile.Id);
            c.Create(company);

            var interestPoint = new InterestPoint("a", "b", "c", "d", "e", "f", "g", true, true, company.Id);
            var category = new CategoryInterestPoint("vegan");
            var interestPointCategory = new InterestPointCategoryInterestPoint(interestPoint.Id, category.Id);

            ipbo.Create(interestPoint);
            cipbo.Create(category);


            var resCreate = ipcipbo.CreateAsync(interestPointCategory).Result;
            var resGet = ipcipbo.ReadAsync(interestPointCategory.Id).Result;

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
            var ipcipbo = new InterestPointCategoryInterestPointBusinessObject();
            var resList = ipcipbo.List();
            var item = resList.Result.FirstOrDefault();
      
            var ipbo = new InterestPointBusinessObject();
            var cipbo = new CategoryInterestPointBusinessObject();
            var pbo = new ProfileBusinessObject();

            var profile = new Profile("II", "AA");
            pbo.Create(profile);

            var c = new CompanyBusinessObject();
            var company = new Company("A", "B", "12345678", "1234567", profile.Id);
            c.Create(company);

            var interestPoint = new InterestPoint("a", "b", "c", "d", "e", "f", "g", true, true, company.Id);
            var category = new CategoryInterestPoint("vegan");       
            
            ipbo.Create(interestPoint);
            cipbo.Create(category);
            var interestPointCategoryInterestPoint = new InterestPointCategoryInterestPoint(interestPoint.Id, category.Id);

            item.InterestPointId = interestPointCategoryInterestPoint.InterestPointId;
            item.CategoryId = interestPointCategoryInterestPoint.CategoryId;
            var resUpdate = ipcipbo.Update(item);
            resList = ipcipbo.List();

            Assert.IsTrue(resUpdate.Success && resList.Success && resList.Result.First().InterestPointId == interestPoint.Id
                && resList.Result.First().CategoryId == category.Id);

        }

        [TestMethod]
        public void TestUpdateInterestpointCategoryAsync()
        {
            BoraNowSeeder.Seed();
            var ipcipbo = new InterestPointCategoryInterestPointBusinessObject();
            var resList = ipcipbo.ListAsync().Result;
            var item = resList.Result.FirstOrDefault();

        
            var ipbo = new InterestPointBusinessObject();
            var cipbo = new CategoryInterestPointBusinessObject();
            var pbo = new ProfileBusinessObject();

            var profile = new Profile("II", "AA");
            pbo.Create(profile);

            var c = new CompanyBusinessObject();
            var company = new Company("A", "B", "12345678", "1234567", profile.Id);
            c.Create(company);

            var interestPoint = new InterestPoint("a", "b", "c", "d", "e", "f", "g", true, true, company.Id);
            var category = new CategoryInterestPoint("vegan");
            ipbo.Create(interestPoint);
            cipbo.Create(category);
            var interestPointCategoryInterestPoint = new InterestPointCategoryInterestPoint(interestPoint.Id, category.Id);

            item.InterestPointId = interestPointCategoryInterestPoint.InterestPointId;
            item.CategoryId = interestPointCategoryInterestPoint.CategoryId;
            var resUpdate = ipcipbo.UpdateAsync(item).Result;
            resList = ipcipbo.ListAsync().Result;

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
