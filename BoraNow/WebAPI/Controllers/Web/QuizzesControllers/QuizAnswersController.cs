using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes;
using WebAPI.Models;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Web.QuizzesControllers
{
    public class QuizAnswersController : Controller
    {
        private readonly QuizAnswerBusinessObject _bo = new QuizAnswerBusinessObject();
        private readonly QuizQuestionBusinessObject _qqbo = new QuizQuestionBusinessObject();
        private readonly QuizBusinessObject _qbo = new QuizBusinessObject();


        
        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListAsync();
            if (!listOperation.Success) return View("Error", new ErrorViewModel() { RequestId = listOperation.Exception.Message });
            var qqListOperation = await _qqbo.ListAsync();
            if (!listOperation.Success) return View("Error", new ErrorViewModel() { RequestId = listOperation.Exception.Message });
            var qListOperation = await _qbo.ListAsync();
            if (!qListOperation.Success) return View("Error", new ErrorViewModel() { RequestId = qListOperation.Exception.Message });

            var QuizAnswerLst = new List<QuizAnswerViewModel>();
            foreach (var item in listOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    QuizAnswerLst.Add(QuizAnswerViewModel.Parse(item));
                }
            }

            var qqLst = new List<QuizQuestionViewModel>();
            foreach (var item in qqListOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    qqLst.Add(QuizQuestionViewModel.Parse(item));
                }
            }
            ViewBag.QuizQuestions = qqLst;
            

            var qLst = new List<QuizViewModel>();
            foreach (var item in qListOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    qLst.Add(QuizViewModel.Parse(item));
                }
            }
            ViewBag.Quizzes = qLst;
            return View(QuizAnswerLst);
        }
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
            if (getOperation.Result == null) return NotFound();
            var vm = QuizAnswerViewModel.Parse(getOperation.Result);
            return View(vm);
        }

        
        public async Task<IActionResult> Create()
        {
            var qqListOperation = await _qqbo.ListAsync();
            var qListOperation = await _qbo.ListAsync();

            if (!qqListOperation.Success) return View("Error", new ErrorViewModel() { RequestId = "Error" });
            var qqList = new List<QuizQuestionViewModel>();
            foreach (var qq in qqListOperation.Result)
            {
                if (!qq.IsDeleted)
                {
                    var qqvm = QuizQuestionViewModel.Parse(qq);
                    qqList.Add(qqvm);
                }
                ViewBag.QuizQuestions = qqList.Select(q => new SelectListItem() { Text = qq.Question, Value = qq.Id.ToString() });
            }

            if (!qListOperation.Success) return View("Error", new ErrorViewModel() { RequestId = "Error" });
            var qList = new List<QuizViewModel>();
            foreach (var q in qListOperation.Result)
            {
                if (!q.IsDeleted)
                {
                    var qvm = QuizViewModel.Parse(q);
                    qList.Add(qvm);
                }
                ViewBag.Quizzes = qList.Select(q => new SelectListItem() { Text = q.Title, Value = q.Id.ToString() });
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Answer","QuizQuestionId", "QuizId")] QuizAnswerViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var QuizAnswer = vm.ToQuizAnswer();
                var qOptions = await _qbo.ListAsync();
                var createOperation = await _bo.CreateAsync(QuizAnswer);
                if (!createOperation.Success) return View("Error", new ErrorViewModel() { RequestId = createOperation.Exception.Message });
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
            if (getOperation.Result == null) return NotFound();
            var vm = QuizAnswerViewModel.Parse(getOperation.Result);
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id, Answer, Question, Quiz")] QuizAnswerViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var getOperation = await _bo.ReadAsync((Guid)id);
                if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
                if (getOperation.Result == null) return NotFound();
                var result = getOperation.Result;
                result.Answer = vm.Answer;
                var updateOperation = await _bo.UpdateAsync(result);
                if (!updateOperation.Success) return View("Error", new ErrorViewModel() { RequestId = updateOperation.Exception.Message });
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var deleteOperation = await _bo.DeleteAsync((Guid)id);
            if (!deleteOperation.Success) return View("Error", new ErrorViewModel() { RequestId = deleteOperation.Exception.Message });
            return RedirectToAction(nameof(Index));
        }
    }
}
