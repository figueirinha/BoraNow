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
    [Route("[controller]")]
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

        [HttpGet]
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

            ViewData["Title"] = "Interest Points";
            ViewData["BreadCrumbs"] = GetCrumbs();
            ViewData["DeleteHref"] = GetDeleteRef();
            ViewBag.Companies = cList;
            return View(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return RecordNotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
            if (getOperation.Result == null) return RecordNotFound();
            var vm = InterestPointViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Interest Point Details";

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "InterestPoints", Icon = "fa-search", Text = "Detail" });

            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }

        [HttpGet("new")]
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
            ViewData["Title"] = "New Interest Point";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "InterestPoints", Icon = "fa-plus", Text = "New" });
            ViewData["BreadCrumbs"] = crumbs;
            return View();
        }

        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Description, Address, PhotoPath, OpeningHours, " +
            "ClosingHours, ClosingDays, CovidSafe, Status, CompanyId")] InterestPointViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var InterestPoint = vm.ToInterestPoint();
                var createOperation = await _bo.CreateAsync(InterestPoint);
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

            var vm = InterestPointViewModel.Parse(getOperation.Result);
            var listCOperation = await _cbo.ListAsync();
            if (!listCOperation.Success) return OperationErrorBackToIndex(listCOperation.Exception);

            var cList = new List<SelectListItem>();
            foreach (var item in listCOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Name };
                    if (item.Id == vm.CompanyId) listItem.Selected = true;
                    cList.Add(listItem);
                }              
            }
            ViewBag.Companies = cList;
            ViewData["Title"] = "Edit Interest Point";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Edit", Controller = "InterestPoints", Icon = "fa-edit", Text = "Edit" });
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id, Name, Description, Address, PhotoPath, OpeningHours, " +
            "ClosingHours, ClosingDays, CovidSafe, Status, CompanyId")] InterestPointViewModel vm)
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
                result.CompanyId = vm.CompanyId;
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