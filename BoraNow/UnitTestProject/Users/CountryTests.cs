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
    public class CountryTests
    {
        [TestMethod]
        public void TestCreateCountry()
        {
            BoraNowSeeder.Seed();
            var vbo = new CountryBusinessObject();

            var country = new Country("madagascar");

            var resCreate = vbo.Create(country);
            var restGet = vbo.Read(country.Id);

            Assert.IsTrue(resCreate.Success && restGet.Success && restGet.Result != null);
        }

        [TestMethod]
        public void TestCreateCountryAsync()
        {
            BoraNowSeeder.Seed();
            var vbo = new CountryBusinessObject();

            var country = new Country("madagascar");

            var resCreate = vbo.CreateAsync(country).Result;
            var restGet = vbo.ReadAsync(country.Id).Result;

            Assert.IsTrue(resCreate.Success && restGet.Success && restGet.Result != null);
        }

        [TestMethod]
        public void TestListCountry()
        {
            BoraNowSeeder.Seed();
            var vbo = new CountryBusinessObject();
            var resList = vbo.List();

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestListCountryAsync()
        {
            BoraNowSeeder.Seed();
            var vbo = new CountryBusinessObject();
            var resList = vbo.ListAsync().Result;

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateCountry()
        {
            BoraNowSeeder.Seed();
            var vbo = new CountryBusinessObject();
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

            var country = new Country("madagascar");

            item.Name = country.Name;
          
            //item.ProfileId = Country.ProfileId;

            var resUpdate = vbo.Update(item);
            resList = vbo.List();

            Assert.IsTrue(resUpdate.Success && resList.Success &&
                resList.Result.First().Name == country.Name);
        }

        [TestMethod]
        public void TestUpdateCountryAsync()
        {
            BoraNowSeeder.Seed();
            var vbo = new CountryBusinessObject();
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

            var country = new Country("madagascar");

            item.Name = country.Name;

            //item.ProfileId = Country.ProfileId;

            var resUpdate = vbo.UpdateAsync(item).Result;
            resList = vbo.ListAsync().Result;

            Assert.IsTrue(resUpdate.Success && resList.Success &&
                resList.Result.First().Name == country.Name);
        }

        [TestMethod]
        public void TestDeleteCountry()
        {
            BoraNowSeeder.Seed();
            var vbo = new CountryBusinessObject();
            var resList = vbo.List();
            var resDelete = vbo.Delete(resList.Result.First().Id);
            resList = vbo.List();

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }

        [TestMethod]
        public void TestDeleteCountryAsync()
        {
            BoraNowSeeder.Seed();
            var vbo = new CountryBusinessObject();
            var resList = vbo.List();
            var resDelete = vbo.DeleteAsync(resList.Result.First().Id).Result;
            resList = vbo.ListAsync().Result;

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }
    }
}
