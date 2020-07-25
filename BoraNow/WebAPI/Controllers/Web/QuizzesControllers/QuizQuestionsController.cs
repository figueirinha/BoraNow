using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.HtmlComponents;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Support;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Web.QuizzesControllers
{
    [ApiExplorerSettings(IgnoreApi = true)]  

    [Route("[controller]")]
    public class QuizQuestionsController : Controller
    {
        private readonly QuizQuestionBusinessObject _bo = new QuizQuestionBusinessObject();
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
                  new BreadCrumb(){Icon = "far fa-file-alt", Action="Index", Controller="QuizQuestions", Text = "Quiz Questions"}
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
            var qListOperation = await _qbo.ListAsync();
            if (!qListOperation.Success) return OperationErrorBackToIndex(qListOperation.Exception);

            var list = new List<QuizQuestionViewModel>();
            foreach (var item in listOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    list.Add(QuizQuestionViewModel.Parse(item));
                }
            }

            var qList = new List<QuizViewModel>();
            foreach (var item in qListOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    qList.Add(QuizViewModel.Parse(item));
                }
            }

            ViewData["Title"] = "Quiz Question";
            ViewData["BreadCrumbs"] = GetCrumbs();
            ViewData["DeleteHref"] = GetDeleteRef();
            ViewBag.Quizzes = qList;
            return View(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return RecordNotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
            if (getOperation.Result == null) return RecordNotFound();

            var getQOperation = await _qbo.ReadAsync(getOperation.Result.QuizId);
            if (!getQOperation.Success) return OperationErrorBackToIndex(getQOperation.Exception);
            if (getQOperation.Result == null) return RecordNotFound();

            var vm = QuizQuestionViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Quiz Question";

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "QuizQuestions", Icon = "fa-search", Text = "Detail" });
            ViewData["Quiz"] = QuizViewModel.Parse(getQOperation.Result);

            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }

       
        [HttpGet("New")]
        public async Task<IActionResult> Create()
        {
            var qListOperation = await _qbo.ListAsync();

            if (!qListOperation.Success) return OperationErrorBackToIndex(qListOperation.Exception);
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

            ViewData["Title"] = "New Question";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "QuizQuestions", Icon = "fa-plus", Text = "New" });
            ViewData["BreadCrumbs"] = crumbs;
            return View();
        }


        [HttpPost("New")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Question", "QuizId")] QuizQuestionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var quizAnswer = vm.ToQuizQuestion();
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

            var vm = QuizQuestionViewModel.Parse(getOperation.Result);
            var listIpOperation = await _qbo.ListAsync();
            if (!listIpOperation.Success) return OperationErrorBackToIndex(listIpOperation.Exception);
            var qList = new List<SelectListItem>();
            foreach (var item in listIpOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Title };
                    if (item.Id == vm.QuizId) listItem.Selected = true;
                    qList.Add(listItem);
                }
            }

            ViewBag.Quizzes = qList;
            ViewData["Title"] = "Edit Quiz Question";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Edit", Controller = "QuizQuestion", Icon = "fa-edit", Text = "Edit" });
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);


            //var vm = QuizQuestionViewModel.Parse(getOperation.Result);
            //ViewData["Title"] = "Edit Question";
            //var crumbs = GetCrumbs();
            //crumbs.Add(new BreadCrumb() { Action = "Edit", Controller = "QuizQuestions", Icon = "fa-edit", Text = "Edit" });
            //ViewData["BreadCrumbs"] = crumbs;
            //return View(vm);
        }


        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id, Question, QuizId")] QuizQuestionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var getOperation = await _bo.ReadAsync((Guid)id);
                if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
                if (getOperation.Result == null) return RecordNotFound();
                var result = getOperation.Result;
                result.Question = vm.Question;
                result.QuizId = vm.QuizId;
                var updateOperation = await _bo.UpdateAsync(result);
                if (!updateOperation.Success) return OperationErrorBackToIndex(updateOperation.Exception);
                else return OperationSuccess("The record was successfuly updated");
            }
            return RedirectToAction(nameof(Index));
        }

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