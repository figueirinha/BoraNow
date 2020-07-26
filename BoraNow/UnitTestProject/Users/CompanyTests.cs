using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Users;
using Recodme.RD.BoraNow.DataAccessLayer.Seeders;
using Recodme.RD.BoraNow.DataLayer.Users;
using System.Linq;

namespace Recodme.RD.BoraNow.UnitTestProject.Users
{
    [TestClass]
    public class CompanyTests
    {
        [TestMethod]
        public void TestCreateCompany()
        {
            BoraNowSeeder.Seed();
            var cbo = new CompanyBusinessObject();
            var pbo = new ProfileBusinessObject();

            var profile = new Profile("EE", "AA");
            pbo.Create(profile);

            var company = new Company("B", "C", "123", "234", profile.Id);

            var resCreate = cbo.Create(company);
            var resGet = cbo.Read(company.Id);

            Assert.IsTrue(resGet.Success && resCreate.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestCreateCompanyAsync()
        {
            BoraNowSeeder.Seed();
            var cbo = new CompanyBusinessObject();
            var pbo = new ProfileBusinessObject();

            var profile = new Profile("EE", "AA");
            pbo.Create(profile);

            var company = new Company("B", "C", "123", "234", profile.Id);


            var resCreate = cbo.CreateAsync(company).Result;
            var resGet = cbo.ReadAsync(company.Id).Result;

            Assert.IsTrue(resGet.Success && resCreate.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListCompany()
        {
            BoraNowSeeder.Seed();
            var cbo = new CompanyBusinessObject();
            var resList = cbo.List();

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);

        }

        [TestMethod]
        public void TestListCompanyAsync()
        {
            BoraNowSeeder.Seed();
            var cbo = new CompanyBusinessObject();
            var resList = cbo.ListAsync().Result;

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);

        }
        [TestMethod]
        public void TestUpdatetCompany()
        {
            BoraNowSeeder.Seed();
            var cbo = new CompanyBusinessObject();
            var resList = cbo.List();
            var item = resList.Result.FirstOrDefault();
            var pbo = new ProfileBusinessObject();

            var profile = new Profile("II", "AA");
            pbo.Create(profile);


            var company = new Company("B", "C", "1263", "2434",profile.Id);

            item.Name = company.Name;
            item.Representative = company.Representative;
            item.PhoneNumber = company.PhoneNumber;
            item.VatNumber = company.VatNumber;
            item.ProfileId = company.ProfileId;

            var resUpdate = cbo.Update(item);
            resList = cbo.List();

            Assert.IsTrue(resUpdate.Success && resList.Success && resList.Result.First().Name == company.Name &&
                resList.Result.First().Representative == company.Representative && resList.Result.First().PhoneNumber == company.PhoneNumber
                 && resList.Result.First().VatNumber == company.VatNumber
                && resList.Result.First().ProfileId == company.ProfileId);
        }

        [TestMethod]
        public void TestUpdateInterestpointCategoryAsync()
        {
            BoraNowSeeder.Seed();
            var cbo = new CompanyBusinessObject();
            var resList = cbo.List();
            var item = resList.Result.FirstOrDefault();

            var pbo = new ProfileBusinessObject();

            var profile = new Profile("II", "AA");
            pbo.Create(profile);


            var company = new Company("B", "C", "1263", "2434", profile.Id);

            item.Name = company.Name;
            item.Representative = company.Representative;
            item.PhoneNumber = company.PhoneNumber;
            item.VatNumber = company.VatNumber;
            item.ProfileId = company.ProfileId;

            var resUpdate = cbo.Update(item);
            resList = cbo.ListAsync().Result;

            Assert.IsTrue(resUpdate.Success && resList.Success && resList.Result.First().Name == company.Name &&
            resList.Result.First().Representative == company.Representative && resList.Result.First().PhoneNumber == company.PhoneNumber
            && resList.Result.First().VatNumber == company.VatNumber
            && resList.Result.First().ProfileId == company.ProfileId);
        }

        [TestMethod]
        public void TestDeleteInterestPointCategory()
        {
            BoraNowSeeder.Seed();
            var bo = new CompanyBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            resList = bo.List();

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);

        }

        [TestMethod]
        public void TestDeleteInterestPointCategoryAsync()
        {
            BoraNowSeeder.Seed();
            var bo = new CompanyBusinessObject();
            var resList = bo.List();
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListAsync().Result;

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }
    }
}
