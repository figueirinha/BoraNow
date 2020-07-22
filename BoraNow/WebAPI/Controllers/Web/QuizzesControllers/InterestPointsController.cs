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
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Suport;
using WebAPI.Models;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Web.QuizzesControllers
{
    public class InterestPointsController : Controller
    {
        private readonly InterestPointBusinessObject _bo = new InterestPointBusinessObject();
        private readonly CompanyBusinessObject _cbo = new CompanyBusinessObject();

        private string GetDeleteRef()
        {
            return this.ControllerContext.RouteData.Values["controller"] + "/" + nameof(Delete);
        }

        private List<BreadCrumb> GetCrumbs()
        {
            return new List<BreadCrumb>()
                { new BreadCrumb(){Icon ="fa-home", Action="Index", Controller="Home", Text="Home"},
                  new BreadCrumb(){Icon = "fa-user-cog", Action="Administration", Controller="Home", Text = "Administration"},
                  new BreadCrumb(){Icon = "fas fa-map-pin", Action="Index", Controller="InterestPoints", Text = "Interest Points"}
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

        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListAsync();
            if (!listOperation.Success) return OperationErrorBackToIndex(listOperation.Exception);
            var cListOperation = await _cbo.ListAsync();
            if (!cListOperation.Success) return OperationErrorBackToIndex(cListOperation.Exception);

            var list = new List<InterestPointViewModel>();
            foreach (var item in listOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    list.Add(InterestPointViewModel.Parse(item));
                }
            }

            var cList = new List<CompanyViewModel>();
            foreach (var item in cListOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    cList.Add(CompanyViewModel.Parse(item));
                }
            }

            ViewData["Title"] = "InterestPoints";
            ViewData["BreadCrumbs"] = GetCrumbs();
            ViewData["DeleteHref"] = GetDeleteRef();
            ViewBag.Companies = cList;
            return View(list);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return RecordNotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
            if (getOperation.Result == null) return RecordNotFound();
            var vm = InterestPointViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "InterestPoint";

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "InterestPoints", Icon = "fa-search", Text = "Detail" });

            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }

        public async Task<IActionResult> Create()
        {
            var cListOperation = await _cbo.ListAsync();
            if (!cListOperation.Success) return OperationErrorBackToIndex(cListOperation.Exception);
            var cList = new List<CompanyViewModel>();
            foreach (var c in cListOperation.Result)
            {
                if (!c.IsDeleted)
                {
                    var cvm = CompanyViewModel.Parse(c);
                    cList.Add(cvm);
                }
                ViewBag.Companies = cList.Select(ip => new SelectListItem() { Text = ip.Name, Value = ip.Id.ToString() });
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Description, Address, PhotoPath, OpeningHours, " +
            "ClosingHours, ClosingDays, CovidSafe, Status, CompanyId")] InterestPointViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var InterestPoint = vm.ToInterestPoint();
                var createOperation = await _bo.CreateAsync(InterestPoint);
                if (!createOperation.Success) return OperationErrorBackToIndex(createOperation.Exception);
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return RecordNotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
            if (getOperation.Result == null) return RecordNotFound();
            var vm = InterestPointViewModel.Parse(getOperation.Result);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id, Name, Description, Address, PhotoPath, OpeningHours, " +
            "ClosingHours, ClosingDays, CovidSafe, Status")] InterestPointViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var getOperation = await _bo.ReadAsync((Guid)id);
                if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
                if (getOperation.Result == null) return RecordNotFound();
                var result = getOperation.Result;
                result.Name = vm.Name;
                result.Description = vm.Description;
                result.Address = vm.Address;
                result.PhotoPath = vm.PhotoPath;
                result.OpeningHours = vm.OpeningHours;
                result.ClosingHours = vm.ClosingHours;
                result.ClosingDays = vm.ClosingDays;
                result.CovidSafe = vm.CovidSafe;
                result.Status = vm.Status;
                var updateOperation = await _bo.UpdateAsync(result);
                if (!updateOperation.Success) return OperationErrorBackToIndex(updateOperation.Exception);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return RecordNotFound();
            var deleteOperation = await _bo.DeleteAsync((Guid)id);
            if (!deleteOperation.Success) return OperationErrorBackToIndex(deleteOperation.Exception);
            return RedirectToAction(nameof(Index));
        }
    }
}