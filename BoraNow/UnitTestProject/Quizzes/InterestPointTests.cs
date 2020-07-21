//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
//using Recodme.RD.BoraNow.DataAccessLayer.Seeders;
//using Recodme.RD.BoraNow.DataLayer.Quizzes;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Recodme.RD.BoraNow.UnitTestProject.Quizzes
//{
//    [TestClass]
//    public class InterestPointTests
//    {
//        [TestMethod]
//        public void TestCreateInterestPoint()
//        {
//            BoraNowSeeder.Seed();
//            var cbo = new InterestPointBusinessObject();

//            var interestPoint = new InterestPoint("a", "b", "c", "d", "e", "f", "g", true, true);

//            var resCreate = cbo.Create(interestPoint);
//            var resGet = cbo.Read(interestPoint.Id);

//            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
//        }

//        [TestMethod]
//        public void TestCreateInterestPointAsync()
//        {
//            BoraNowSeeder.Seed();
//            var cbo = new InterestPointBusinessObject();

//            var interestPoint = new InterestPoint("a", "b", "c", "d", "e", "f", "g", true, true);

//            var resCreate = cbo.CreateAsync(interestPoint).Result;
//            var resGet = cbo.ReadAsync(interestPoint.Id).Result;

//            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
//        }

//        [TestMethod]
//        public void TestListInterestPoint()
//        {
//            BoraNowSeeder.Seed();
//            var bo = new InterestPointBusinessObject();
//            var resList = bo.List();

//            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
//        }

//        [TestMethod]
//        public void TestListInterestPointAsync()
//        {
//            BoraNowSeeder.Seed();
//            var bo = new InterestPointBusinessObject();
//            var resList = bo.ListAsync().Result;

//            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
//        }

//        [TestMethod]
//        public void TestUpdateInterestPoint()
//        {
//            BoraNowSeeder.Seed();

//            var ipbo = new InterestPointBusinessObject();
//            var resList = ipbo.List();
//            var item = resList.Result.FirstOrDefault();

//            var interestPoint = new InterestPoint("a", "b", "c", "d", "e", "f", "g", true, true);

//            ipbo.Create(interestPoint);

//            item.Name = interestPoint.Name;
//            item.Address = interestPoint.Address;
//            item.ClosingDays = interestPoint.ClosingDays;
//            item.ClosingHours = interestPoint.ClosingHours;
//            item.Description = interestPoint.Description;
//            item.OpeningHours = interestPoint.OpeningHours;
//            item.PhotoPath = interestPoint.PhotoPath;
//            item.CovidSafe = interestPoint.CovidSafe;
//            item.Status = interestPoint.Status;

//            var resUpdate = ipbo.Update(item);
//            resList = ipbo.List();

//            Assert.IsTrue(resUpdate.Success && resList.Success && resList.Result.First().Name == interestPoint.Name
//                && resList.Result.First().Address == interestPoint.Address
//                && resList.Result.First().ClosingHours == interestPoint.ClosingHours
//                && resList.Result.First().Description == interestPoint.Description
//                && resList.Result.First().ClosingDays == interestPoint.ClosingDays
//                && resList.Result.First().OpeningHours == interestPoint.OpeningHours
//                && resList.Result.First().PhotoPath == interestPoint.PhotoPath
//                && resList.Result.First().CovidSafe == interestPoint.CovidSafe
//                && resList.Result.First().Status == interestPoint.Status);
//        }

//        [TestMethod]
//        public void TestUpdateInterestPointAsync()
//        {
//            BoraNowSeeder.Seed();

//            var ipbo = new InterestPointBusinessObject();
//            var resList = ipbo.List();
//            var item = resList.Result.FirstOrDefault();

//            var interestPoint = new InterestPoint("a", "b", "c", "d", "e", "f", "g", true, true);

//            ipbo.Create(interestPoint);

//            item.Name = interestPoint.Name;
//            item.Address = interestPoint.Address;
//            item.ClosingDays = interestPoint.ClosingDays;
//            item.ClosingHours = interestPoint.ClosingHours;
//            item.Description = interestPoint.Description;
//            item.OpeningHours = interestPoint.OpeningHours;
//            item.PhotoPath = interestPoint.PhotoPath;
//            item.CovidSafe = interestPoint.CovidSafe;
//            item.Status = interestPoint.Status;

//            var resUpdate = ipbo.UpdateAsync(item).Result;
//            resList = ipbo.ListAsync().Result;

//            Assert.IsTrue(resUpdate.Success && resList.Success && resList.Result.First().Name == interestPoint.Name
//                && resList.Result.First().Address == interestPoint.Address
//                && resList.Result.First().ClosingHours == interestPoint.ClosingHours
//                && resList.Result.First().Description == interestPoint.Description
//                && resList.Result.First().ClosingDays == interestPoint.ClosingDays
//                && resList.Result.First().OpeningHours == interestPoint.OpeningHours
//                && resList.Result.First().PhotoPath == interestPoint.PhotoPath
//                && resList.Result.First().CovidSafe == interestPoint.CovidSafe
//                && resList.Result.First().Status == interestPoint.Status);
//        }

//        [TestMethod]
//        public void TestDeleteInterestPoint()
//        {
//            BoraNowSeeder.Seed();
//            var bo = new InterestPointBusinessObject();
//            var resList = bo.List();
//            var resDelete = bo.Delete(resList.Result.First().Id);
//            resList = bo.List();

//            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
//        }

//        [TestMethod]
//        public void TestDeleteInterestPointAsync()
//        {
//            BoraNowSeeder.Seed();
//            var bo = new InterestPointBusinessObject();
//            var resList = bo.List();
//            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
//            resList = bo.ListAsync().Result;

//            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
//        }

//    }
//}
