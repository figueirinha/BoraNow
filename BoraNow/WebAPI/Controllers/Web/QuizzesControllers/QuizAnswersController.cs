using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.HtmlComponents;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Suport;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Web.QuizzesControllers
{
    [ApiExplorerSettings(IgnoreApi = true)]

    [Route("[controller]")]
    public class QuizAnswersController : Controller
    {
        private readonly QuizAnswerBusinessObject _bo = new QuizAnswerBusinessObject();
        private readonly QuizQuestionBusinessObject _qqbo = new QuizQuestionBusinessObject();
        private readonly QuizBusinessObject _qbo = new QuizBusinessObject();


        private string GetDeleteRef()
        {
            return this.ControllerContext.RouteData.Values["controller"] + "/" + nameof(Delete);
        }

        private List<BreadCrumb> GetCrumbs()
        {
            return new List<BreadCrumb>()
                { new BreadCrumb(){Icon ="fa-home", Action="Index", Controller="Home", Text="Home"},
                  new BreadCrumb(){Icon = "fa-user-cog", Action="Administration", Controller="Home", Text = "Administration"},
                  new BreadCrumb(){Icon = "far fa-file-alt", Action="Index", Controller="QuizAnswers", Text = "Quiz Answer"}
                };
        }

        private IActionResult RecordNotFound()
        {
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Information, "The record was not found");
            return RedirectToAction(nameof(Index));
        }

        private IActionResult OperationErrorBackToIndex(Exception exception)
        {
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, exception);
            return RedirectToAction(nameof(Index));
        }

        private IActionResult OperationSuccess(string message)
        {
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Success, message);
            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListAsync();
            if (!listOperation.Success) return OperationErrorBackToIndex(listOperation.Exception);
            var qqListOperation = await _qqbo.ListAsync();
            if (!qqListOperation.Success) return OperationErrorBackToIndex(qqListOperation.Exception);
            var qListOperation = await _qbo.ListAsync();
            if (!qListOperation.Success) return OperationErrorBackToIndex(qListOperation.Exception);

            var list = new List<QuizAnswerViewModel>();
            foreach (var item in listOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    list.Add(QuizAnswerViewModel.Parse(item));
                }
                
            }

            var qqList = new List<QuizQuestionViewModel>();
            foreach (var item in qqListOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    qqList.Add(QuizQuestionViewModel.Parse(item));
                }
            }

            ViewData["Title"] = "Quiz Answer";
            ViewData["BreadCrumbs"] = GetCrumbs();
            ViewData["DeleteHref"] = GetDeleteRef();
            ViewBag.QuizQuestions = qqList;
            return View(list);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return RecordNotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
            if (getOperation.Result == null) return RecordNotFound();

            var getQQOperation = await _qqbo.ReadAsync(getOperation.Result.QuizQuestionId);
            if (!getQQOperation.Success) return OperationErrorBackToIndex(getQQOperation.Exception);
            if (getQQOperation.Result == null) return RecordNotFound();

            var vm = QuizAnswerViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Quiz Answer";

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "QuizAnswers", Icon = "fa-search", Text = "Detail" });
            ViewData["QuizQuestion"] = QuizQuestionViewModel.Parse(getQQOperation.Result);

            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }

        [HttpGet("New")]
        public async Task<IActionResult> Create()
        {
            var qqListOperation = await _qqbo.ListAsync();

            if (!qqListOperation.Success) return OperationErrorBackToIndex(qqListOperation.Exception);
            var qqList = new List<QuizQuestionViewModel>();
            foreach (var qq in qqListOperation.Result)
            {
                if (!qq.IsDeleted)
                {
                    var qqvm = QuizQuestionViewModel.Parse(qq);
                    qqList.Add(qqvm);
                }
                ViewBag.QuizQuestions = qqList.Select(qq => new SelectListItem() { Text = qq.Question, Value = qq.Id.ToString() });
            }

            ViewData["Title"] = "New Answer";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "QuizAnswers", Icon = "fa-plus", Text = "New" });
            ViewData["BreadCrumbs"] = crumbs;
            return View();
            
        }

        [HttpPost("New")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Answer","QuizQuestionId", "QuizId")] QuizAnswerViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var quizAnswer = vm.ToQuizAnswer();
                var createOperation = await _bo.CreateAsync(quizAnswer);
                if (!createOperation.Success) return OperationErrorBackToIndex(createOperation.Exception);
                return OperationSuccess("The record was successfuly created");
            }
            return View(vm);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return RecordNotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
            if (getOperation.Result == null) return RecordNotFound();
            var vm = QuizAnswerViewModel.Parse(getOperation.Result);
            var listIpOperation = await _qqbo.ListAsync();
            if (!listIpOperation.Success) return OperationErrorBackToIndex(listIpOperation.Exception);
            var qqList = new List<SelectListItem>();
            foreach (var item in listIpOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Question };
                    if (item.Id == vm.QuizQuestionId) listItem.Selected = true;
                    qqList.Add(listItem);
                }
            }

            ViewBag.QuizQuestions = qqList;
            ViewData["Title"] = "Edit Quiz Answer";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Edit", Controller = "QuizAnswers", Icon = "fa-edit", Text = "Edit" });
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id, Answer, QuizQuestionId")] QuizAnswerViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var getOperation = await _bo.ReadAsync((Guid)id);
                if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
                if (getOperation.Result == null) return RecordNotFound();
                var result = getOperation.Result;
                result.Answer = vm.Answer;
                result.QuizQuestionId = vm.QuizQuestionId;
                var updateOperation = await _bo.UpdateAsync(result);
                if (!updateOperation.Success)
                {
                    TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, updateOperation.Exception);
                    return View(vm);
                }
                else return OperationSuccess("The record was successfuly updated");
            }
            return RedirectToAction(nameof(Index));
        }

        //[HttpGet("edit/{id}")]
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null) return RecordNotFound();
        //    var getOperation = await _bo.ReadAsync((Guid)id);
        //    if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
        //    if (getOperation.Result == null) return RecordNotFound();
        //    var vm = QuizAnswerViewModel.Parse(getOperation.Result);
        //    ViewData["Title"] = "Edit Answer";
        //    var crumbs = GetCrumbs();
        //    crumbs.Add(new BreadCrumb() { Action = "Edit", Controller = "QuizAnswers", Icon = "fa-edit", Text = "Edit" });
        //    ViewData["BreadCrumbs"] = crumbs;
        //    return View(vm);
        //}

        //[HttpPost("edit/{id}")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("Id, Answer, Question")] QuizAnswerViewModel vm)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var getOperation = await _bo.ReadAsync((Guid)id);
        //        if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
        //        if (getOperation.Result == null) return RecordNotFound();
        //        var result = getOperation.Result;
        //        result.Answer = vm.Answer;

        //        result.QuizQuestionId = vm.QuizQuestionId;
        //        var updateOperation = await _bo.UpdateAsync(result);
        //        if (!updateOperation.Success) return OperationErrorBackToIndex(updateOperation.Exception);
        //        else return OperationSuccess("The record was successfuly updated");
        //    }
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return RecordNotFound();
            var deleteOperation = await _bo.DeleteAsync((Guid)id);
            if (!deleteOperation.Success) return OperationErrorBackToIndex(deleteOperation.Exception);
            return OperationSuccess("The record was successfuly deleted");
        }
    }
}
