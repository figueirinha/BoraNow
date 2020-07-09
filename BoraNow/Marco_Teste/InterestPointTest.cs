using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.DataLayer.Quizzes;

namespace Marco_Teste
{
    public class OperationResult { public bool Success { get; set; } }


    [TestClass]
    public class InterestPointTest
    {

        [TestMethod]
        public void TestCreatInterestPoint()
        {
            var ip = new InterestPoint("Estação do Rossio", "Estação de comboio", "rua do Rossio", "C:/foto", "9:00", 
                                                                                        "21:00", "SAB", true, true);
            var bo = new InterestPointBusinessObject();
            bo.Create(ip);
            var result = new OperationResult() { Success = true };
            var ipCreated = bo.Read(ip.Id);
            Assert.IsTrue(ipCreated.Result.Address == ip.Address && ipCreated.Result.ClosingDays == ip.ClosingDays 
                && ipCreated.Result.ClosingHours == ip.ClosingHours && ipCreated.Result.CovidSafe == ip.CovidSafe 
                && ipCreated.Result.Description == ip.Description && ipCreated.Result.PhotoPath == ip.PhotoPath 
                && ipCreated.Result.Status == ip.Status && ipCreated.Result.Name == ip.Name);
        }

        [TestMethod]
        public void TestUpdatInterestPoint()
        {
            var newIp = new InterestPoint("Estação do Areeiro", "Estação de metro", "Rua do Areeiro", "C:/foto/Areeiro", "6:00",
                                                                                        "01:00", "SEG", false, false);
            var bo = new InterestPointBusinessObject();
            var ip = bo.List().Result[0];
            ip.Name = newIp.Name;
            ip.Description = newIp.Description;
            ip.Address = newIp.Address;
            ip.PhotoPath = newIp.PhotoPath;
            ip.OpeningHours = newIp.OpeningHours;
            ip.ClosingHours = newIp.ClosingHours;
            ip.ClosingDays = newIp.ClosingDays;
            ip.CovidSafe = newIp.CovidSafe;
            ip.Status = newIp.Status;
            bo.Update(ip);
            ip = bo.List().Result[0];
            Assert.IsTrue(ip.Address == newIp.Address && ip.ClosingDays == newIp.ClosingDays
                && ip.ClosingHours == newIp.ClosingHours && ip.CovidSafe == newIp.CovidSafe
                && ip.Description == newIp.Description && ip.PhotoPath == newIp.PhotoPath
                && ip.Status == newIp.Status && ip.Name == newIp.Name);


        }

        [TestMethod]
        public void TestDeleteInterest()
        {
            var bo = new InterestPointBusinessObject();
            var ip = bo.List().Result[0];
            var oldId = ip.Id;
            bo.Delete(ip.Id);
            ip = bo.List().Result[0];
            Assert.IsTrue(ip.Id == oldId); 
        }

    }
}
