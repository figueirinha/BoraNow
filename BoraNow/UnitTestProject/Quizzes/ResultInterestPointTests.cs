using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Users;
using Recodme.RD.BoraNow.DataAccessLayer.Seeders;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using Recodme.RD.BoraNow.DataLayer.Users;
using System;
using System.Linq;

namespace Recodme.RD.BoraNow.UnitTestProject.Quizzes
{
    [TestClass]

    public class ResultInterestPointTests
    {
        [TestMethod]
        public void TestCreateResultInterestPoint()
        {
            BoraNowSeeder.Seed();
            var ripbo = new ResultInterestPointBusinessObject();
            var rbo = new ResultBusinessObject();
            var ipbo = new InterestPointBusinessObject();
            var vbo = new VisitorBusinessObject();

            var qbo = new QuizBusinessObject();
            var quiz = new Quiz("Quiz 1");
            qbo.Create(quiz);

            var countrybo = new CountryBusinessObject();
            var pbo = new ProfileBusinessObject();
            var companybo = new CompanyBusinessObject();

            var country = new Country("Holanda");
            var profile = new Profile("a", "b");
            var company = new Company("a", "b", "c", "d", profile.Id);
            countrybo.Create(country);
            pbo.Create(profile);
            companybo.Create(company);


            var visitor = new Visitor("A", "C", DateTime.Now, "M", profile.Id, country.Id);
            vbo.Create(visitor);


            var result = new Result("Quiz 1", DateTime.UtcNow, quiz.Id, visitor.Id);
   
            var interestPoint = new InterestPoint("Bar do Rui", "Pesticos&Cocktails", "Rua dos Anjos", "C://images", "14h", "00h", "Sabados", true, true, company.Id);
            rbo.Create(result);
            ipbo.Create(interestPoint);

            var _resultInterestPoint = new ResultInterestPoint(result.Id, interestPoint.Id);

            var resCreate = ripbo.Create(_resultInterestPoint);
            var resGet = ripbo.Read(_resultInterestPoint.Id);
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);

        }

        [TestMethod]
        public void TestCreateResultInterestPointAsync()
        {
            var ripbo = new ResultInterestPointBusinessObject();
            var rbo = new ResultBusinessObject();
            var ipbo = new InterestPointBusinessObject();
     
            var vbo = new VisitorBusinessObject();

            var qbo = new QuizBusinessObject();
            var quiz = new Quiz("Quiz 1");
            qbo.Create(quiz);

            var countrybo = new CountryBusinessObject();
            var pbo = new ProfileBusinessObject();
            var companybo = new CompanyBusinessObject();

            var country = new Country("Holanda");
            var profile = new Profile("a", "b");
            var company = new Company("a", "b", "c", "d", profile.Id);
            countrybo.Create(country);
            pbo.Create(profile);
            companybo.Create(company);


            var visitor = new Visitor("A", "C", DateTime.Now, "M", profile.Id, country.Id);
            vbo.Create(visitor);
       
            var result = new Result("Quiz 1", DateTime.UtcNow, quiz.Id, visitor.Id);

            var interestPoint = new InterestPoint("Bar do Rui", "Pesticos&Cocktails", "Rua dos Anjos", "C://images", "14h", "00h", "Sabados", true, true, company.Id);
            rbo.Create(result);
            ipbo.Create(interestPoint);

            var _resultInterestPoint = new ResultInterestPoint(result.Id, interestPoint.Id);


            var resCreate = ripbo.CreateAsync(_resultInterestPoint).Result;
            var resGet = ripbo.ReadAsync(_resultInterestPoint.Id).Result;
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);

        }

        [TestMethod]
        public void TestListResultInterestPoint()
        {
            BoraNowSeeder.Seed();
            var bo = new ResultInterestPointBusinessObject();
            var resList = bo.List();
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestListResultInterestPointAsync()
        {
            BoraNowSeeder.Seed();
            var bo = new ResultInterestPointBusinessObject();
            var resList = bo.ListAsync().Result;
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateResultInterestPoint()
        {
            BoraNowSeeder.Seed();
            var ripbo = new ResultInterestPointBusinessObject();
            var resList = ripbo.List();
            var item = resList.Result.FirstOrDefault();

            var rbo = new ResultBusinessObject();
            var ipbo = new InterestPointBusinessObject();
        
            var vbo = new VisitorBusinessObject();

            var qbo = new QuizBusinessObject();
            var quiz = new Quiz("Quiz 1");
            qbo.Create(quiz);

            var countrybo = new CountryBusinessObject();
            var pbo = new ProfileBusinessObject();
            var companybo = new CompanyBusinessObject();

            var country = new Country("Holanda");
            var profile = new Profile("a", "b");
            var company = new Company("a", "b", "c", "d", profile.Id);
            countrybo.Create(country);
            pbo.Create(profile);
            companybo.Create(company);


            var visitor = new Visitor("A", "E", DateTime.Now, "M", profile.Id, country.Id);
            vbo.Create(visitor);
       
            var result = new Result("Quiz 2", DateTime.UtcNow, quiz.Id, visitor.Id);

            var interestPoint = new InterestPoint("Bar do Rui", "Pesticos&Cocktails", "-", "C://images", "14h", "00h", "D", true, true, company.Id);
            rbo.Create(result);
            ipbo.Create(interestPoint);

            var resultInterestPoint = new ResultInterestPoint(result.Id, interestPoint.Id);

            item.ResultId = resultInterestPoint.ResultId;
            item.InterestPointId = resultInterestPoint.InterestPointId;
            var resUpdate = ripbo.Update(item);
            resList = ripbo.List();

            Assert.IsTrue(resList.Success && resUpdate.Success &&
               resList.Result.First().ResultId == item.ResultId && resList.Result.First().InterestPointId == item.InterestPointId);

        }

        [TestMethod]
        public void TestUpdateResultInterestPointAsync()
        {
            BoraNowSeeder.Seed();
            var ripbo = new ResultInterestPointBusinessObject();
            var resList = ripbo.List();
            var item = resList.Result.FirstOrDefault();

            var rbo = new ResultBusinessObject();
            var ipbo = new InterestPointBusinessObject();

            var vbo = new VisitorBusinessObject();

            var qbo = new QuizBusinessObject();
            var quiz = new Quiz("Quiz 1");
            qbo.Create(quiz);

            var countrybo = new CountryBusinessObject();
            var pbo = new ProfileBusinessObject();
            var companybo = new CompanyBusinessObject();

            var country = new Country("Holanda");
            var profile = new Profile("a", "b");
            var company = new Company("a", "b", "c", "d", profile.Id);
            countrybo.Create(country);
            pbo.Create(profile);
            companybo.Create(company);


            var visitor = new Visitor("A", "E", DateTime.Now, "M", profile.Id, country.Id);
            vbo.Create(visitor);

            var result = new Result("Quiz 2", DateTime.UtcNow, quiz.Id, visitor.Id);

            var interestPoint = new InterestPoint("Bar do Rui", "Pesticos&Cocktails", "-", "C://images", "14h", "00h", "D", true, true, company.Id);
            rbo.Create(result);
            ipbo.Create(interestPoint);

            var resultInterestPoint = new ResultInterestPoint(result.Id, interestPoint.Id);

            item.ResultId = resultInterestPoint.ResultId;
            item.InterestPointId = resultInterestPoint.InterestPointId;
            var resUpdate = ripbo.UpdateAsync(item).Result;
            resList = ripbo.ListAsync().Result;

            Assert.IsTrue(resList.Success && resUpdate.Success &&
               resList.Result.First().ResultId == item.ResultId && resList.Result.First().InterestPointId == item.InterestPointId);

        }

        [TestMethod]
        public void TesDeletetResultInterestPoint()
        {
            BoraNowSeeder.Seed();
            var bo = new ResultInterestPointBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            resList = bo.List();

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }


        [TestMethod]
        public void TesDeletetResultInterestPointAsync()
        {
            BoraNowSeeder.Seed();
            var bo = new ResultInterestPointBusinessObject();
            var resList = bo.List();
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListAsync().Result;

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.First().IsDeleted);
        }
    }
}
