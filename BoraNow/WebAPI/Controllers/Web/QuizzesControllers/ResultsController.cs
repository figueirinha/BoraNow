using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Users;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Users;
using WebAPI.Models;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Web.QuizzesControllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ResultsController : Controller
    {
        private readonly ResultBusinessObject _rbo = new ResultBusinessObject();
        private readonly QuizBusinessObject _qbo = new QuizBusinessObject();
        private readonly VisitorBusinessObject _vbo = new VisitorBusinessObject();
        public async Task<IActionResult> Index()
        {
            var listOperation = await _rbo.ListAsync();
            if (!listOperation.Success) return View("Error", new ErrorViewModel() { RequestId = listOperation.Exception.Message });

            var quizListOperation = await _qbo.ListAsync();
            if (!quizListOperation.Success) return View("Error", new ErrorViewModel() { RequestId = quizListOperation.Exception.Message });

            var visitorListOperation = await _vbo.ListAsync();
            if (!visitorListOperation.Success) return View("Error", new ErrorViewModel() { RequestId = visitorListOperation.Exception.Message });


            var resultList = new List<ResultViewModel>();
            foreach (var item in listOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    resultList.Add(ResultViewModel.Parse(item));
                }
            }
            var quizList = new List<QuizViewModel>();
            foreach (var item in quizListOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    quizList.Add(QuizViewModel.Parse(item));
                }
            }
            var visitorList = new List<QuizViewModel>();
            foreach (var item in quizListOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    visitorList.Add(QuizViewModel.Parse(item));
                }
            }

            ViewBag.Quizzes = quizList;
            ViewBag.Visitors = visitorList;
            return View(resultList);
        }
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var getOperation = await _rbo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
            if (getOperation.Result == null) return NotFound();

            if (id == null) return NotFound();
            var getQuizOperation = await _qbo.ReadAsync((Guid)id);
            if (!getQuizOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getQuizOperation.Exception.Message });
            if (getQuizOperation.Result == null) return NotFound();

            if (id == null) return NotFound();
            var getVisitorOperation = await _vbo.ReadAsync((Guid)id);
            if (!getVisitorOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getVisitorOperation.Exception.Message });
            if (getVisitorOperation.Result == null) return NotFound();

            var vm = ResultViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Result";
            ViewData["Quiz"] = QuizViewModel.Parse(getQuizOperation.Result);
            ViewData["Visitor"] = ResultViewModel.Parse(getOperation.Result);

            return View(vm);
        }
        public async Task<IActionResult> Create()
        {
            var quizListOperation = await _qbo.ListAsync();
            if (!quizListOperation.Success) return View("Error", new ErrorViewModel() { RequestId = "Error" });
            var quizList = new List<QuizViewModel>();
            foreach (var quiz in quizListOperation.Result)
            {
                if (!quiz.IsDeleted)
                {
                    var quizVm = QuizViewModel.Parse(quiz);
                    quizList.Add(quizVm);
                }
                ViewBag.Quizzes = quizList.Select(dr => new SelectListItem() { Text = quiz.Title, Value = quiz.Id.ToString() });
            }
            var visitorListOperation = await _vbo.ListAsync();
            if (!visitorListOperation.Success) return View("Error", new ErrorViewModel() { RequestId = "Error" });
            var visitorList = new List<VisitorViewModel>();
            foreach (var visitor in visitorListOperation.Result)
            {
                if (!visitor.IsDeleted)
                {
                    var visitorVm = VisitorViewModel.Parse(visitor);
                    visitorList.Add(visitorVm);
                }
                ViewBag.Visitors = visitorList.Select(visitor => new SelectListItem() { Text = visitor.FirstName, Value = visitor.Id.ToString() });
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title", "Date", "QuizId", "VisitorId")] ResultViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = vm.ToResult();
                var createOperation = await _rbo.CreateAsync(result);
                if (!createOperation.Success) return View("Error", new ErrorViewModel() { RequestId = createOperation.Exception.Message });
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null) return NotFound();
        //    var getOperation = await _bo.ReadAsync((Guid)id);
        //    if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
        //    if (getOperation.Result == null) return NotFound();
        //    var vm = ResultViewModel.Parse(getOperation.Result);
        //    return View(vm);
        //}
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(Guid id, [Bind("Id, Name, QuizId")] ResultViewModel vm)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var getOperation = await _bo.ReadAsync((Guid)id);
        //            if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
        //            if (getOperation.Result == null) return NotFound();
        //            var result = getOperation.Result;
        //            result.Name = vm.Name;
        //            result.QuizId = vm.QuizId;
        //            var updateOperation = await _bo.UpdateAsync(result);
        //            if (!updateOperation.Success) return View("Error", new ErrorViewModel() { RequestId = updateOperation.Exception.Message });
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var deleteOperation = await _rbo.DeleteAsync((Guid)id);
            if (!deleteOperation.Success) return View("Error", new ErrorViewModel() { RequestId = deleteOperation.Exception.Message });
            return RedirectToAction(nameof(Index));
        }
    }
}