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

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Web.UserControllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[controller]")]
    public class ProfilesController : Controller
    {
        private readonly ProfileBusinessObject _bo = new ProfileBusinessObject();
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
                  new BreadCrumb(){Icon = "fas fa-id-card", Action="Index", Controller="´Profiles", Text = "Profile"}
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
            var lst = new List<ProfileViewModel>();
            foreach (var item in listOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    lst.Add(ProfileViewModel.Parse(item));
                }
            }
            var clistOperation = await _cbo.ListAsync();
            if (!clistOperation.Success) return OperationErrorBackToIndex(clistOperation.Exception);
            var clst = new List<CountryViewModel>();
            foreach (var item in clistOperation.Result)
            {
                if (!item.IsDeleted)
                {
                   clst.Add(CountryViewModel.Parse(item));
                }
            }

            ViewData["Title"] = "Profile";
            ViewData["Breadcrumbs"] = GetCrumbs();
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

            var getCountryOperation = await _cbo.ReadAsync(getOperation.Result.CountryId);
            if (!getCountryOperation.Success) return OperationErrorBackToIndex(getCountryOperation.Exception);
            if (getCountryOperation.Result == null) return RecordNotFound();
 

            var vm = ProfileViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Profile - Details";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Details", Controller = "Profiles", Icon = "fa-search", Text = "Detail" });
            ViewData["Countries"] = CountryViewModel.Parse(getCountryOperation.Result);
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }
        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
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


            ViewData["Title"] = "New Profile";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Create", Controller = "Profiles", Icon = "fa-plus", Text = "New" });
            ViewData["BreadCrumbs"] = crumbs;
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description, PhotoPath, CountryId")] ProfileViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var profile = vm.ToProfile();
                var createOperation = await _bo.CreateAsync(profile);
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

            var vm = ProfileViewModel.Parse(getOperation.Result);
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

            ViewData["Title"] = "Edit Profile";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Edit", Controller = "Profiles", Icon = "fa-edit", Text = "Edit" });
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Description, PhotoPath,CountryId")] ProfileViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var getOperation = await _bo.ReadAsync((Guid)id);
                if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
                if (getOperation.Result == null) return RecordNotFound();
                var result = getOperation.Result;
                result.Description = vm.Description;
                result.PhotoPath = vm.PhotoPath;
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
            else return OperationSuccess("The record was successfuly deleted");
        }
    }
}
