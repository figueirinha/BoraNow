using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Meteo;
using Recodme.RD.BoraNow.DataAccessLayer.Seeders;
using Recodme.RD.BoraNow.DataLayer.Meteo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recodme.RD.BoraNow.UnitTestProject.Meteo
{
    [TestClass]
    public class MeteorologyTests
    {
        [TestMethod]
        public void TestCreaeMeeteorology()
        {
            BoraNowSeeder.Seed();
            var mbo = new MeteorologyBusinessObject();

            var meteo = new Meteorology(10, 11, 50, 2, 7, DateTime.Now.AddDays(200));

            var resCreate = mbo.Create(meteo);
            var restGet = mbo.Read(meteo.Id);

            Assert.IsTrue(resCreate.Success && restGet.Success && restGet.Result != null);
        }

        [TestMethod]
        public void TestCreateMeteorologyAsync()
        {
            BoraNowSeeder.Seed();
            var mbo = new MeteorologyBusinessObject();

            var meteo = new Meteorology(10, 11, 50, 2, 7, DateTime.Now.AddDays(200));

            var resCreate = mbo.CreateAsync(meteo).Result;
            var restGet = mbo.ReadAsync(meteo.Id).Result;

            Assert.IsTrue(resCreate.Success && restGet.Success && restGet.Result != null);
        }

        [TestMethod]
        public void TestListMeteorology()
        {
            BoraNowSeeder.Seed();
            var mbo = new MeteorologyBusinessObject();
            var resList = mbo.List();

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestListMeteorologyAsync()
        {
            BoraNowSeeder.Seed();
            var mbo = new MeteorologyBusinessObject();
            var resList = mbo.ListAsync().Result;

            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateMeteorology()
        {
            BoraNowSeeder.Seed();
            var mbo = new MeteorologyBusinessObject();
            var resList = mbo.List();
            var item = resList.Result.FirstOrDefault();

            var meteo = new Meteorology(10, 11, 50, 2, 7, DateTime.Now.AddDays(200));

            item.MaxTemperature = meteo.MaxTemperature;
            item.MinTemperature = meteo.MinTemperature;
            item.RainPercentage = meteo.RainPercentage;
            item.UvIndex = meteo.UvIndex;
            item.WindIndex = meteo.WindIndex;
            item.Date = meteo.Date;

            var resUpdate = mbo.Update(item);
            resList = mbo.List();

            Assert.IsTrue(resUpdate.Success && resList.Success && 
                resList.Result.First().MaxTemperature == meteo.MaxTemperature && resList.Result.First().MinTemperature == meteo.MinTemperature
                && resList.Result.First().RainPercentage == meteo.RainPercentage && resList.Result.First().UvIndex == meteo.UvIndex
                && resList.Result.First().WindIndex == meteo.WindIndex && resList.Result.First().Date == meteo.Date);
        }

        [TestMethod]
        public void TestUpdateMeteorologyAsync()
        {
            BoraNowSeeder.Seed();
            var mbo = new MeteorologyBusinessObject();
            var resList = mbo.List();
            var item = resList.Result.FirstOrDefault();

            var meteo = new Meteorology(10, 11, 50, 2, 7, DateTime.Now.AddDays(200));

            item.MaxTemperature = meteo.MaxTemperature;
            item.MinTemperature = meteo.MinTemperature;
            item.RainPercentage = meteo.RainPercentage;
            item.UvIndex = meteo.UvIndex;
            item.WindIndex = meteo.WindIndex;
            item.Date = meteo.Date;

            var resUpdate = mbo.UpdateAsync(item).Result;
            resList = mbo.ListAsync().Result;

            Assert.IsTrue(resUpdate.Success && resList.Success &&
                resList.Result.First().MaxTemperature == meteo.MaxTemperature && resList.Result.First().MinTemperature == meteo.MinTemperature
                && resList.Result.First().RainPercentage == meteo.RainPercentage && resList.Result.First().UvIndex == meteo.UvIndex
                && resList.Result.First().WindIndex == meteo.WindIndex && resList.Result.First().Date == meteo.Date);
        }

        [TestMethod]
        public void TestDeleteMeteorology()
        {
            BoraNowSeeder.Seed();
            var mbo = new MeteorologyBusinessObject();
            var resList = mbo.List();
            var resDelete = mbo.Delete(resList.Result.First().Id);
            resList = mbo.List();

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }

        [TestMethod]
        public void TestDeleteMeteorologyAsync()
        {
            BoraNowSeeder.Seed();
            var mbo = new MeteorologyBusinessObject();
            var resList = mbo.List();
            var resDelete = mbo.DeleteAsync(resList.Result.First().Id).Result;
            resList = mbo.ListAsync().Result;

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }
             
    }
}
