using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Users;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.HtmlComponents;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Users;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Support;


namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Web.QuizzesControllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[controller]")]
    public class ResultsController : Controller
    {
        private readonly ResultBusinessObject _bo = new ResultBusinessObject();
        private readonly QuizBusinessObject _qbo = new QuizBusinessObject();
        private readonly VisitorBusinessObject _vbo = new VisitorBusinessObject();

        private string GetDeleteRef()
        {
            return this.ControllerContext.RouteData.Values["controller"] + "/" + nameof(Delete);
        }

        private List<BreadCrumb> GetCrumbs()
        {
            return new List<BreadCrumb>()
                { new BreadCrumb(){Icon ="fa-home", Action="Index", Controller="Home", Text="Home"},
                  new BreadCrumb(){Icon = "fa-user-cog", Action="Administration", Controller="Home", Text = "Administration"},
                  new BreadCrumb(){Icon = "fas fa-poll", Action="Index", Controller="Resuts", Text = "Result"}
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
            var lst = new List<ResultViewModel>();
            foreach (var item in listOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    lst.Add(ResultViewModel.Parse(item));
                }
            }
            var qlistOperation = await _qbo.ListAsync();
            if (!qlistOperation.Success) return OperationErrorBackToIndex(qlistOperation.Exception);
            var quizlst = new List<QuizViewModel>();
            foreach (var item in qlistOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    quizlst.Add(QuizViewModel.Parse(item));
                }
            }
            var vlistOperation = await _vbo.ListAsync();
            if (!vlistOperation.Success) return OperationErrorBackToIndex(vlistOperation.Exception);
            var visitorlst = new List<VisitorViewModel>();
            foreach (var item in vlistOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    visitorlst.Add(VisitorViewModel.Parse(item));
                }
            }

            ViewData["Title"] = "Results Quiz";
            ViewData["Breadcrumbs"] = GetCrumbs();
            ViewData["DeleteHref"] = GetDeleteRef();
            ViewData["Quizzes"] = quizlst;
            ViewData["Visitors"] = visitorlst;
            return View(lst);
            
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
            if (getOperation.Result == null) return NotFound();


            var vm = ResultViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Result Quiz -  Details";

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Create", Controller = "Results", Icon = "fa-search", Text = "Detail" });

            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }
        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            var qListOperation = await _qbo.ListAsync();
            if (!qListOperation.Success) return OperationErrorBackToIndex(qListOperation.Exception);
            var quizList = new List<QuizViewModel>();
            foreach (var item in qListOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    var qvm = QuizViewModel.Parse(item);
                    quizList.Add(qvm);
                }
                ViewBag.Quizzes = quizList.Select(r => new SelectListItem() { Text = r.Title, Value = r.Id.ToString() });
            }
            var vListOperation = await _vbo.ListAsync();
            if (!vListOperation.Success) return OperationErrorBackToIndex(vListOperation.Exception);
            var visitorList = new List<VisitorViewModel>();
            foreach (var item in vListOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    var vvm = VisitorViewModel.Parse(item);
                    visitorList.Add(vvm);
                }
                ViewBag.Visitors = visitorList.Select(r => new SelectListItem() { Text = r.FirstName, Value = r.Id.ToString() });
            }

            ViewData["Title"] = "New Results";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Create", Controller = "Results", Icon = "fa-plus", Text = "New" });
            ViewData["BreadCrumbs"] = crumbs;
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title, Date, QuizId, VisitorId")] ResultViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = vm.ToResult();
                var createOperation = await _bo.CreateAsync(result);
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

            var vm = ResultViewModel.Parse(getOperation.Result);
            var listQOperation = await _qbo.ListAsync();
            if (!listQOperation.Success) return OperationErrorBackToIndex(listQOperation.Exception);
            var qList = new List<SelectListItem>();
            foreach (var item in listQOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Title };
                    if (item.Id == vm.QuizId) listItem.Selected = true;
                    qList.Add(listItem);
                }
            }
            var listVOperation = await _vbo.ListAsync();
            if (!listVOperation.Success) return OperationErrorBackToIndex(listQOperation.Exception);
            var vList = new List<SelectListItem>();
            foreach (var item in listVOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.FirstName };
                    if (item.Id == vm.VisitorId) listItem.Selected = true;
                    vList.Add(listItem);
                }
            }
            ViewBag.Quizzes = qList;
            ViewBag.Visitors = vList;
            ViewData["Title"] = "Edit Result";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Edit", Controller = "Results", Icon = "fa-edit", Text = "Edit" });
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title, Date")] ResultViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var getOperation = await _bo.ReadAsync((Guid)id);
                if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
                if (getOperation.Result == null) return RecordNotFound();
                var result = getOperation.Result;
                result.Title = vm.Title;
                result.Date = vm.Date;
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
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return RecordNotFound();
            var deleteOperation = await _bo.DeleteAsync((Guid)id);
            if (!deleteOperation.Success) return OperationErrorBackToIndex(deleteOperation.Exception);
            else return OperationSuccess("The record was successfuly deleted");
        }
    }
}