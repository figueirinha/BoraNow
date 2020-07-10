using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.DataLayer.Quizzes;

namespace UnitTestProject
{

    [TestClass]
    public class CategoryTests
    {
        [TestMethod]
        public void TestCreateCategory()
        {
            var _category = new Category("Praia");
            var _bo = new CategoryBusinessObject();
            _bo.Create(_category);
            var _categoryCreated = _bo.Read(_category.Id);
            Assert.IsTrue(_categoryCreated.Result.Name == _category.Name);
        }
        [TestMethod]
        public void TestUpdateCategory()
        {
            var newNameCategory = "Bar";
            var _bo = new CategoryBusinessObject();
            var _category = _bo.List().Result[0];
            _category.Name = newNameCategory;
            _bo.Update(_category);
            _category = _bo.List().Result[0];
            Assert.IsTrue(_category.Name == newNameCategory);
        }
        [TestMethod]
        public void TestDeleteCategoryId()
        {
            var _bo = new CategoryBusinessObject();
            var _category = _bo.List().Result[0];
            var existingId = _category.Id;
            _bo.Delete(_category.Id);
            _category = _bo.List().Result[0];
            Assert.IsTrue(_category.Id == existingId);
        }

    }
}
