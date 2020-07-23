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
    public class InterestPointCategoryInterestPointsController : Controller
    {
        private readonly InterestPointCategoryInterestPointBusinessObject _bo = new InterestPointCategoryInterestPointBusinessObject();
        private readonly InterestPointBusinessObject _ipbo = new InterestPointBusinessObject();
        private readonly CategoryInterestPointBusinessObject _cipbo = new CategoryInterestPointBusinessObject();

        private string GetDeleteRef()
        {
            return this.ControllerContext.RouteData.Values["controller"] + "/" + nameof(Delete);
        }

        private List<BreadCrumb> GetCrumbs()
        {
            return new List<BreadCrumb>()
                { new BreadCrumb(){Icon ="fa-home", Action="Index", Controller="Home", Text="Home"},
                  new BreadCrumb(){Icon = "fa-user-cog", Action="Administration", Controller="Home", Text = "Administration"},
                  new BreadCrumb(){Icon = "fas fa-map-pin", Action="Index", Controller="InterestPointCategoryInterestPoints", Text = "Interest Points Category - Interest Point"}
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
            var cipListOperation = await _cipbo.ListAsync();
            if (!cipListOperation.Success) return OperationErrorBackToIndex(cipListOperation.Exception);


            var list = new List<InterestPointCategoryInterestPointViewModel>();
            foreach (var item in listOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    list.Add(InterestPointCategoryInterestPointViewModel.Parse(item));
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
            var cipList = new List<CategoryInterestPointViewModel>();
            foreach (var item in cipListOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    cipList.Add(CategoryInterestPointViewModel.Parse(item));
                }
            }

            ViewData["Title"] = "Interest Point Category - Interest Points";
            ViewData["BreadCrumbs"] = GetCrumbs();
            ViewData["DeleteHref"] = GetDeleteRef();
            ViewBag.InterestPoints = ipList;
            ViewBag.CategoryInterestPoints = cipList;
            return View(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return RecordNotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
            if (getOperation.Result == null) return RecordNotFound();
            var vm = InterestPointCategoryInterestPointViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Interest Point Category - InterestPoint";

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "InterestPointCategoryInterestPoints", Icon = "fa-search", Text = "Detail" });

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
            var cipListOperation = await _cipbo.ListAsync();
            if (!cipListOperation.Success) return OperationErrorBackToIndex(cipListOperation.Exception);
            var cipList = new List<CategoryInterestPointViewModel>();
            foreach (var cip in cipListOperation.Result)
            {
                if (!cip.IsDeleted)
                {
                    var cipvm = CategoryInterestPointViewModel.Parse(cip);
                    cipList.Add(cipvm);
                }
                ViewBag.CategpryInterestPoints = cipList.Select(icip => new SelectListItem() { Text = icip.Name, Value = icip.Id.ToString() });
            }

            ViewData["Title"] = "New Interest Point Category - Interest Point";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "InterestPointCategoryInterestPoints", Icon = "fa-plus", Text = "New" });
            ViewData["BreadCrumbs"] = crumbs;
            return View();
        }

        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InterestPointId, CategoryId")] InterestPointCategoryInterestPointViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var interestPointCategoryInterestPoint = vm.ToInterestPointCategoryInteresPoint();
                var createOperation = await _bo.CreateAsync(interestPointCategoryInterestPoint);
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

            var vm = InterestPointCategoryInterestPointViewModel.Parse(getOperation.Result);
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
            var listCipOperation = await _cipbo.ListAsync();
            if (!listCipOperation.Success) return OperationErrorBackToIndex(listCipOperation.Exception);
            var cipList = new List<SelectListItem>();
            foreach (var item in listCipOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Name };
                    if (item.Id == vm.InterestPointId) listItem.Selected = true;
                    cipList.Add(listItem);
                }
            }

            ViewBag.InterestPoints = ipList;
            ViewBag.CategoryInterestPoints = cipList;

            ViewData["Title"] = "Edit Interest Point Category - Interest Points";

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Edit", Controller = "InterestPointsCategoryInterestPoints", Icon = "fa-edit", Text = "Edit" });
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id, InterestPointId, CategoryId")] InterestPointCategoryInterestPointViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var getOperation = await _bo.ReadAsync((Guid)id);
                if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
                if (getOperation.Result == null) return RecordNotFound();
                var result = getOperation.Result;
                result.InterestPointId = vm.InterestPointId;
                result.CategoryId = vm.CategoryId;
              
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
