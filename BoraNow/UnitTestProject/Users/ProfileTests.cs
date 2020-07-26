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
    public class ProfileTests
    {
        [TestMethod]
        public void TestCreateProfile()
        {
            BoraNowSeeder.Seed();
            var pbo = new ProfileBusinessObject();

            var profile = new Profile("B", "C");

            var resCreate = pbo.Create(profile);
            var resGet = pbo.Read(profile.Id);

            Assert.IsTrue(resGet.Success && resCreate.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestCreateProfileAsync()
        {
            BoraNowSeeder.Seed();
            var pbo = new ProfileBusinessObject();
        

            var profile = new Profile("B", "C");

            var resCreate = pbo.CreateAsync(profile).Result;
            var resGet = pbo.ReadAsync(profile.Id).Result;

            Assert.IsTrue(resGet.Success && resCreate.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListProfile()
        {
            BoraNowSeeder.Seed();
            var pbo = new ProfileBusinessObject();
            var resList = pbo.List();

            Assert.IsTrue(resList.Success && resList.Result.Count == 2);

        }

        [TestMethod]
        public void TestListProfileAsync()
        {
            BoraNowSeeder.Seed();
            var pbo = new ProfileBusinessObject();
            var resList = pbo.ListAsync().Result;

            Assert.IsTrue(resList.Success && resList.Result.Count == 2);

        }
        [TestMethod]
        public void TestUpdatetProfile()
        {
            BoraNowSeeder.Seed();
            var pbo = new ProfileBusinessObject();
            var resList = pbo.List();
            var item = resList.Result.FirstOrDefault();

            var profile = new Profile("E", "A");

            item.Description = profile.Description;
            item.PhotoPath = profile.PhotoPath;
          

            var resUpdate = pbo.Update(item);
            resList = pbo.List();

            Assert.IsTrue(resUpdate.Success && resList.Success && resList.Result.First().Description == profile.Description &&
                resList.Result.First().PhotoPath == profile.PhotoPath);
        }

        [TestMethod]
        public void TestUpdateInterestpointCategoryAsync()
        {
            BoraNowSeeder.Seed();
            var pbo = new ProfileBusinessObject();
            var resList = pbo.List();
            var item = resList.Result.FirstOrDefault();


            var profile = new Profile("E", "A");

            item.Description = profile.Description;
            item.PhotoPath = profile.PhotoPath;
         

            var resUpdate = pbo.Update(item);
            resList = pbo.ListAsync().Result;

            Assert.IsTrue(resUpdate.Success && resList.Success && resList.Result.First().Description == profile.Description &&
                          resList.Result.First().PhotoPath == profile.PhotoPath);
        }

        [TestMethod]
        public void TestDeleteInterestPointCategory()
        {
            BoraNowSeeder.Seed();
            var bo = new ProfileBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            resList = bo.List();

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);

        }

        [TestMethod]
        public void TestDeleteInterestPointCategoryAsync()
        {
            BoraNowSeeder.Seed();
            var bo = new ProfileBusinessObject();
            var resList = bo.List();
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListAsync().Result;

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }
    }
}
