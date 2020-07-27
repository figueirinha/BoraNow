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
    public class CompaniesController : Controller
    {
        private readonly CompanyBusinessObject _bo = new CompanyBusinessObject();
        private readonly ProfileBusinessObject _pbo = new ProfileBusinessObject();

        private string GetDeleteRef()
        {
            return this.ControllerContext.RouteData.Values["controller"] + "/" + nameof(Delete);
        }

        private List<BreadCrumb> GetCrumbs()
        {
            return new List<BreadCrumb>()
                { new BreadCrumb(){Icon ="fa-home", Action="Index", Controller="Home", Text="Home"},
                  new BreadCrumb(){Icon = "fa-user-cog", Action="Administration", Controller="Home", Text = "Administration"},
                  new BreadCrumb(){Icon = "fas fa-building", Action="Index", Controller="Companies", Text = "Companies"}
                };
        }
        private IActionResult RecordNotFound()
        {
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Information, "The record was not found");
            return RedirectToAction("Index", "Home");
        }

        private IActionResult OperationErrorBackToIndex(Exception exception)
        {
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, exception);
            return RedirectToAction("Index", "Home");
        }

        private IActionResult OperationSuccess(string message)
        {
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Success, message);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListAsync();
            if (!listOperation.Success) return OperationErrorBackToIndex(listOperation.Exception);

            var pListOperation = await _pbo.ListAsync();
            if (!pListOperation.Success) return OperationErrorBackToIndex(pListOperation.Exception);

            var lst = new List<CompanyViewModel>();
            foreach (var item in listOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    lst.Add(CompanyViewModel.Parse(item));
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

            ViewBag.PRofile = pList;
            ViewData["Title"] = "Companies";
            ViewData["BreadCrumbs"] = GetCrumbs();
            ViewData["DeleteHref"] = GetDeleteRef();

            return View(lst);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return RecordNotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception); ;
            if (getOperation.Result == null) return RecordNotFound();

            var getPOperation = await _pbo.ReadAsync(getOperation.Result.ProfileId);
            if (!getPOperation.Success) return OperationErrorBackToIndex(getPOperation.Exception);
            if (getPOperation.Result == null) return RecordNotFound();

            var vm = CompanyViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Company Details";

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "Companies", Icon = "fa-search", Text = "Detail" });

            ViewData["Profiles"] = ProfileViewModel.Parse(getPOperation.Result);
            ViewData["BreadCrumbs"] = crumbs;


            return View(vm);
        }
        [HttpGet("Create")]
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
            ViewData["Title"] = "New Company ";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "Companies", Icon = "fa-plus", Text = "New" });
            ViewData["BreadCrumbs"] = crumbs;
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Representative, PhoneNumber, VatNumber, ProfileId")] CompanyViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var c = vm.ToCompany();
                var createOperation = await _bo.CreateAsync(c);
                if (!createOperation.Success) return OperationErrorBackToIndex(createOperation.Exception);
                else return OperationSuccess("The company account was successfuly registered!");
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

            var vm = CompanyViewModel.Parse(getOperation.Result);

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
            ViewData["Title"] = "Edit Company";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Edit", Controller = "Companies", Icon = "fa-edit", Text = "Edit" });
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name, Representative, PhoneNumber, VatNumber, ProfileId")] CompanyViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var getOperation = await _bo.ReadAsync((Guid)id);
                if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
                if (getOperation.Result == null) return RecordNotFound();
                var result = getOperation.Result;
                if (!vm.CompareToModel(result))
                {
                    result.Name = vm.Name;
                    result.Representative = vm.Representative;
                    result.PhoneNumber = vm.PhoneNumber;
                    result.VatNumber = vm.VatNumber;
                    result.ProfileId = vm.ProfileId;
                    var updateOperation = await _bo.UpdateAsync(result);
                    if (!updateOperation.Success)
                    {
                        TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, updateOperation.Exception);
                        return View(vm);
                    }
                    else return OperationSuccess("The record was successfuly updated");
                }
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