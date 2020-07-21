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
    public class VisitorTests
    {
        [TestMethod]
        public void TestCreateVisitor()
        {
            BoraNowSeeder.Seed();
            var vbo = new VisitorBusinessObject();

            //var countrybo = new CountryBusinessObject();
            //var pbo = new ProfileBusinessObject();
            //var companybo = new CompanyBusinessObject();

            //var country = new Country("narnia");
            //var profile = new Profile("a", "b", country.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()); 
            //var company = new Company("a", "b", "c", "d", profile.Id);
            //countrybo.Create(country);
            //pbo.Create(profile);
            //companybo.Create(company);

            var visitor = new Visitor("m", "f", DateTime.Now.AddYears(-24), "m");

            var resCreate = vbo.Create(visitor);
            var restGet = vbo.Read(visitor.Id);

            Assert.IsTrue(resCreate.Success && restGet.Success && restGet.Result != null);
        }

        [TestMethod]
        public void TestCreateVisitorAsync()
        {
            BoraNowSeeder.Seed();
            var vbo = new VisitorBusinessObject();

            //var countrybo = new CountryBusinessObject();
            //var pbo = new ProfileBusinessObject();
            //var companybo = new CompanyBusinessObject();

            //var country = new Country("narnia");
            //var profile = new Profile("a", "b", country.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            //var company = new Company("a", "b", "c", "d", profile.Id);
            //countrybo.Create(country);
            //pbo.Create(profile);
            //companybo.Create(company);

            var visitor = new Visitor("m", "f", DateTime.Now.AddYears(-24), "m");

            var resCreate = vbo.CreateAsync(visitor).Result;
            var restGet = vbo.ReadAsync(visitor.Id).Result;

            Assert.IsTrue(resCreate.Success && restGet.Success && restGet.Result != null);
        }

        [TestMethod]
        public void TestListVisitor()
        {
            BoraNowSeeder.Seed();
            var vbo = new VisitorBusinessObject();
            var resList = vbo.List();

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestListVisitorAsync()
        {
            BoraNowSeeder.Seed();
            var vbo = new VisitorBusinessObject();
            var resList = vbo.ListAsync().Result;

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateVisitor()
        {
            BoraNowSeeder.Seed();
            var vbo = new VisitorBusinessObject();
            var resList = vbo.List();
            var item = resList.Result.FirstOrDefault();

            //var countrybo = new CountryBusinessObject();
            //var pbo = new ProfileBusinessObject();
            //var companybo = new CompanyBusinessObject();

            //var country = new Country("narnia");
            //var profile = new Profile("a", "b", country.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            //var company = new Company("a", "b", "c", "d", profile.Id);
            //countrybo.Create(country);
            //pbo.Create(profile);
            //companybo.Create(company);

            var visitor = new Visitor("m", "f", DateTime.Now.AddYears(-24), "m");

            item.FirstName = visitor.FirstName;
            item.LastName = visitor.LastName;
            item.BirthDate = visitor.BirthDate;
            item.Gender = visitor.Gender;
            //item.ProfileId = visitor.ProfileId;

            var resUpdate = vbo.Update(item);
            resList = vbo.List();

            Assert.IsTrue(resUpdate.Success && resList.Success &&
                resList.Result.First().FirstName == visitor.FirstName && resList.Result.First().LastName == visitor.LastName &&
                resList.Result.First().BirthDate == visitor.BirthDate && resList.Result.First().Gender == visitor.Gender 
                /*&& resList.Result.First().ProfileId == visitor.ProfileId*/);
        }

        [TestMethod]
        public void TestUpdateVisitorAsync()
        {
            BoraNowSeeder.Seed();
            var vbo = new VisitorBusinessObject();
            var resList = vbo.List();
            var item = resList.Result.FirstOrDefault();

            //var countrybo = new CountryBusinessObject();
            //var pbo = new ProfileBusinessObject();
            //var companybo = new CompanyBusinessObject();

            //var country = new Country("narnia");
            //var profile = new Profile("a", "b", country.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            //var company = new Company("a", "b", "c", "d", profile.Id);
            //countrybo.Create(country);
            //pbo.Create(profile);
            //companybo.Create(company);

            var visitor = new Visitor("m", "f", DateTime.Now.AddYears(-24), "m"/*, profile.Id*/);

            item.FirstName = visitor.FirstName;
            item.LastName = visitor.LastName;
            item.BirthDate = visitor.BirthDate;
            item.Gender = visitor.Gender;
            //item.ProfileId = visitor.ProfileId;

            var resUpdate = vbo.UpdateAsync(item).Result;
            resList = vbo.ListAsync().Result;

            Assert.IsTrue(resUpdate.Success && resList.Success &&
                resList.Result.First().FirstName == visitor.FirstName && resList.Result.First().LastName == visitor.LastName &&
                resList.Result.First().BirthDate == visitor.BirthDate && resList.Result.First().Gender == visitor.Gender 
                /*&& resList.Result.First().ProfileId == visitor.ProfileId*/);
        }

        [TestMethod]
        public void TestDeleteVisitor()
        {
            BoraNowSeeder.Seed();
            var vbo = new VisitorBusinessObject();
            var resList = vbo.List();
            var resDelete = vbo.Delete(resList.Result.First().Id);
            resList = vbo.List();

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }

        [TestMethod]
        public void TestDeleteVisitorAsync()
        {
            BoraNowSeeder.Seed();
            var vbo = new VisitorBusinessObject();
            var resList = vbo.List();
            var resDelete = vbo.DeleteAsync(resList.Result.First().Id).Result;
            resList = vbo.ListAsync().Result;

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }
    }
}
