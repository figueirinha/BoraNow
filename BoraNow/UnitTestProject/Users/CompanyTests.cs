using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Users;
using Recodme.RD.BoraNow.DataAccessLayer.Seeders;
using Recodme.RD.BoraNow.DataLayer.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            var company = new Company("B", "C", "123", "234");

            var resCreate = cbo.Create(company);
            var resGet = cbo.Read(company.Id);

            Assert.IsTrue(resGet.Success && resCreate.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestCreateCompanyAsync()
        {
            BoraNowSeeder.Seed();
            var cbo = new CompanyBusinessObject();

            var company = new Company("B", "C", "123", "234");


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


            var company = new Company("B", "F", "1263", "2434");

            item.Name = company.Name;
            item.Representative = company.Representative;
            item.PhoneNumber = company.PhoneNumber;
            item.VatNumber = company.VatNumber;

            var resUpdate = cbo.Update(item);
            resList = cbo.List();

            Assert.IsTrue(resUpdate.Success && resList.Success && resList.Result.First().Name == company.Name &&
                resList.Result.First().Representative == company.Representative && resList.Result.First().PhoneNumber == company.PhoneNumber && resList.Result.First().VatNumber == company.VatNumber);

        }

        [TestMethod]
        public void TestUpdateInterestpointCategoryAsync()
        {
            BoraNowSeeder.Seed();
            var cbo = new CompanyBusinessObject();
            var resList = cbo.List();
            var item = resList.Result.FirstOrDefault();

            var company = new Company("B", "F", "1263", "2434");

            item.Name = company.Name;
            item.Representative = company.Representative;
            item.PhoneNumber = company.PhoneNumber;
            item.VatNumber = company.VatNumber;

            var resUpdate = cbo.Update(item);
            resList = cbo.ListAsync().Result;

            Assert.IsTrue(resUpdate.Success && resList.Success && resList.Result.First().Name == company.Name &&
            resList.Result.First().Representative == company.Representative && resList.Result.First().PhoneNumber == company.PhoneNumber && resList.Result.First().VatNumber == company.VatNumber);
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
