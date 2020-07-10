using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.DataAccessLayer.Seeders;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recodme.RD.BoraNow.UnitTestProject.Quizzes
{
    [TestClass]

    public class ResultTests
    {
        [TestMethod]
        public void TestCreateResult()
        {
            BoraNowSeeder.Seed();
            var _boQuiz = new QuizBusinessObject();
            var _bo = new ResultBusinessObject();

            var quiz = new Quiz("Q1");
            var _result = new Result("Resulta Q1", DateTime.UtcNow, quiz.Id);
            var resCreate = _bo.Create(_result);
            var resGet = _bo.Read(_result.Id);
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);

        }
    }
}
