using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Users;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.HtmlComponents;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Users;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Support;
using WebAPI.Models;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Web.UserControllers
{
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class VisitorsController : Controller
    {
        private readonly VisitorBusinessObject _bo = new VisitorBusinessObject();
        private readonly ProfileBusinessObject _pbo = new ProfileBusinessObject();
        private readonly CountryBusinessObject _cbo = new CountryBusinessObject();


        private string GetDeleteRef()
        {
            return this.ControllerContext.RouteData.Values["controller"] + "/" + nameof(Delete);
        }

        private List<BreadCrumb> GetCrumbs()
        {
            return new List<BreadCrumb>()
                { new BreadCrumb(){Icon ="fa-home", Action="Index", Controller="Home", Text="Home"},
                  new BreadCrumb(){Icon = "fa-user-cog", Action="Administration", Controller="Home", Text = "Administration"},
                  new BreadCrumb(){Icon = "fas fa-hiking", Action="Index", Controller="Visitors", Text = "Visitors"}
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

            var pListOperation = await _pbo.ListAsync();
            if (!pListOperation.Success) return OperationErrorBackToIndex(pListOperation.Exception);

            var clistOperation = await _cbo.ListAsync();
            if (!clistOperation.Success) return OperationErrorBackToIndex(clistOperation.Exception);       

            var lst = new List<VisitorViewModel>();
            foreach (var item in listOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    lst.Add(VisitorViewModel.Parse(item));
                }
            }

            var pList = new List<ProfileViewModel>();
            foreach (var item in pListOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    pList.Add(ProfileViewModel.Parse(item));
                }
            }

            var clst = new List<CountryViewModel>();
            foreach (var item in clistOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    clst.Add(CountryViewModel.Parse(item));
                }
            }

            ViewBag.Profiles = pList;
            ViewBag.Countries = clst;

            ViewData["Title"] = "Visitors";
            ViewData["BreadCrumbs"] = GetCrumbs();
            ViewData["DeleteHref"] = GetDeleteRef();
            ViewData["Countries"] = clst;

            return View(lst);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return RecordNotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
            if (getOperation.Result == null) return RecordNotFound();

            var getPOperation = await _pbo.ReadAsync(getOperation.Result.ProfileId);
            if (!getPOperation.Success) return OperationErrorBackToIndex(getPOperation.Exception);
            if (getPOperation.Result == null) return RecordNotFound();

            var getCountryOperation = await _cbo.ReadAsync(getOperation.Result.CountryId);
            if (!getCountryOperation.Success) return OperationErrorBackToIndex(getCountryOperation.Exception);
            if (getCountryOperation.Result == null) return RecordNotFound();

            var vm = VisitorViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Visitor Details";

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "Visitors", Icon = "fa-search", Text = "Detail" });

            ViewData["Profiles"] = ProfileViewModel.Parse(getPOperation.Result);
            ViewData["Countries"] = CountryViewModel.Parse(getCountryOperation.Result);
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }

        [HttpGet("new")]
        public async Task<IActionResult> Create()
        {
            var pListOperation = await _pbo.ListAsync();
            if (!pListOperation.Success) return OperationErrorBackToIndex(pListOperation.Exception);
            var pList = new List<ProfileViewModel>();
            foreach (var c in pListOperation.Result)
            {
                if (!c.IsDeleted)
                {
                    var cvm = ProfileViewModel.Parse(c);
                    pList.Add(cvm);
                }
                ViewBag.Profiles = pList.Select(p => new SelectListItem() { Text = p.Description, Value = p.Id.ToString() });
            }
            var cListOperation = await _cbo.ListAsync();
            if (!cListOperation.Success) return OperationErrorBackToIndex(cListOperation.Exception);
            var cList = new List<CountryViewModel>();
            foreach (var item in cListOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    var cvm = CountryViewModel.Parse(item);
                    cList.Add(cvm);
                }
                ViewBag.Countries = cList.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() });
            }
            ViewData["Title"] = "Create Visitor";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "Visitors", Icon = "fa-plus", Text = "New" });
            ViewData["BreadCrumbs"] = crumbs;
            return View();
        }

        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName, LastName, BirthDate, Gender, ProfileId, CountryId")] VisitorViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var Visitor = vm.ToVisitor();
                var createOperation = await _bo.CreateAsync(Visitor);
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

            var vm = VisitorViewModel.Parse(getOperation.Result);

            var listPOperation = await _pbo.ListAsync();
            if (!listPOperation.Success) return OperationErrorBackToIndex(listPOperation.Exception);

            var pList = new List<SelectListItem>();
            foreach (var item in listPOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Description };
                    if (item.Id == vm.ProfileId) listItem.Selected = true;
                    pList.Add(listItem);
                }
            }
            ViewBag.Profiles = pList;

            var listCountryOperation = await _cbo.ListAsync();
            if (!listCountryOperation.Success) return OperationErrorBackToIndex(listCountryOperation.Exception);
            var cList = new List<SelectListItem>();
            foreach (var item in listCountryOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Name };
                    if (item.Id == vm.CountryId) listItem.Selected = true;
                    cList.Add(listItem);
                }
            }

            ViewBag.Countries = cList;

            ViewData["Title"] = "Edit Visitor";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Edit", Controller = "Visitors", Icon = "fa-edit", Text = "Edit" });
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id, FirstName, LastName, BirthDate, Gender, ProfileId, CountryId")] VisitorViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var getOperation = await _bo.ReadAsync((Guid)id);
                if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
                if (getOperation.Result == null) return RecordNotFound();
                var result = getOperation.Result;
                result.FirstName = vm.FirstName;
                result.LastName = vm.LastName;
                result.BirthDate = vm.BirthDate;
                result.Gender = vm.Gender;
                result.ProfileId = vm.ProfileId;
                result.CountryId = vm.CountryId;
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