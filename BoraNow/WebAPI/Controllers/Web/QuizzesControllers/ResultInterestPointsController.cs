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
using WebAPI.Models;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Web.QuizzesControllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[controller]")]
    public class ResultInterestPointsController : Controller
    {
        private readonly ResultInterestPointBusinessObject _bo = new ResultInterestPointBusinessObject();
        private readonly InterestPointBusinessObject _ipbo = new InterestPointBusinessObject();
        private readonly ResultBusinessObject _rbo = new ResultBusinessObject();

        private string GetDeleteRef()
        {
            return this.ControllerContext.RouteData.Values["controller"] + "/" + nameof(Delete);
        }

        private List<BreadCrumb> GetCrumbs()
        {
            return new List<BreadCrumb>()
                { new BreadCrumb(){Icon ="fa-home", Action="Index", Controller="Home", Text="Home"},
                  new BreadCrumb(){Icon = "fa-user-cog", Action="Administration", Controller="Home", Text = "Administration"},
                  new BreadCrumb(){Icon = "fas fa-map-pin", Action="Index", Controller="ResultInterestPoints", Text = "ResultInterest Points"}
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
            var ipListOperation = await _ipbo.ListAsync();
            if (!ipListOperation.Success) return OperationErrorBackToIndex(ipListOperation.Exception);
            var rListOperation = await _rbo.ListAsync();
            if (!rListOperation.Success) return OperationErrorBackToIndex(rListOperation.Exception);


            var list = new List<ResultInterestPointViewModel>();
            foreach (var item in listOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    list.Add(ResultInterestPointViewModel.Parse(item));
                }
            }

            var ipList = new List<InterestPointViewModel>();
            foreach (var item in ipListOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    ipList.Add(InterestPointViewModel.Parse(item));
                }
            }
            var rList = new List<ResultViewModel>();
            foreach (var item in rListOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    rList.Add(ResultViewModel.Parse(item));
                }
            }

            ViewData["Title"] = "Interest Point Result ";
            ViewData["BreadCrumbs"] = GetCrumbs();
            ViewData["DeleteHref"] = GetDeleteRef();
            ViewBag.InterestPoints = ipList;
            ViewBag.Results = rList;
            return View(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return RecordNotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);

            if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
            if (getOperation.Result == null) return RecordNotFound();

            var getIpOperation = await _ipbo.ReadAsync(getOperation.Result.InterestPointId);
            if (!getIpOperation.Success) return OperationErrorBackToIndex(getIpOperation.Exception);
            if (getIpOperation.Result == null) return RecordNotFound();

            var getROperation = await _rbo.ReadAsync(getOperation.Result.ResultId);
            if (!getROperation.Success) return OperationErrorBackToIndex(getROperation.Exception);
            if (getROperation.Result == null) return RecordNotFound();

            var vm = ResultInterestPointViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Interest Point Result";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Details", Controller = "ResultInterestPoints", Icon = "fa-search", Text = "Detail" });
            ViewData["InterestPoints"] = InterestPointViewModel.Parse(getIpOperation.Result);
            ViewData["Results"] = ResultViewModel.Parse(getROperation.Result);
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }

        [HttpGet("new")]
        public async Task<IActionResult> Create()
        {
            var ipListOperation = await _ipbo.ListAsync();
            if (!ipListOperation.Success) return OperationErrorBackToIndex(ipListOperation.Exception);
            var ipList = new List<InterestPointViewModel>();
            foreach (var ip in ipListOperation.Result)
            {
                if (!ip.IsDeleted)
                {
                    var ipvm = InterestPointViewModel.Parse(ip);
                    ipList.Add(ipvm);
                }
                ViewBag.InterestPoints = ipList.Select(icip => new SelectListItem() { Text = icip.Name, Value = icip.Id.ToString() });
            }
            var rListOperation = await _rbo.ListAsync();
            if (!rListOperation.Success) return OperationErrorBackToIndex(rListOperation.Exception);
            var rList = new List<ResultViewModel>();
            foreach (var result in rListOperation.Result)
            {
                if (!result.IsDeleted)
                {
                    var rvm = ResultViewModel.Parse(result);
                    rList.Add(rvm);
                }
                ViewBag.Results = rList.Select(rip => new SelectListItem() { Text = rip.Title, Value = rip.Id.ToString() });
            }

            ViewData["Title"] = "New Interest Point Result";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "ResultInterestPoints", Icon = "fa-plus", Text = "New" });
            ViewData["BreadCrumbs"] = crumbs;
            return View();
        }

        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InterestPointId, ResultId")] ResultInterestPointViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var rip = vm.ToResultInterestPoint();
                var createOperation = await _bo.CreateAsync(rip);
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

            var vm = ResultInterestPointViewModel.Parse(getOperation.Result);

            var listIpOperation = await _ipbo.ListAsync();
            if (!listIpOperation.Success) return OperationErrorBackToIndex(listIpOperation.Exception);
            var ipList = new List<SelectListItem>();
            foreach (var item in listIpOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Name };
                    if (item.Id == vm.InterestPointId) listItem.Selected = true;
                    ipList.Add(listItem);
                }
            }
            var listROperation = await _rbo.ListAsync();
            if (!listROperation.Success) return OperationErrorBackToIndex(listROperation.Exception);
            var rList = new List<SelectListItem>();
            foreach (var item in listROperation.Result)
            {
                if (!item.IsDeleted)
                {
                    var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Title };
                    if (item.Id == vm.InterestPointId) listItem.Selected = true;
                    rList.Add(listItem);
                }
            }

            ViewBag.InterestPoints = ipList;
            ViewBag.Results = rList;

            ViewData["Title"] = "Edit Interest Point Result";

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Edit", Controller = "ResultInterestPoints", Icon = "fa-edit", Text = "Edit" });
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id, InterestPointId, ResultId")] ResultInterestPointViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var getOperation = await _bo.ReadAsync((Guid)id);
                if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
                if (getOperation.Result == null) return RecordNotFound();
                var result = getOperation.Result;
                result.InterestPointId = vm.InterestPointId;
                result.ResultId = vm.ResultId;

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
            return OperationSuccess("The record was successfuly deleted");
        }
    }
}

