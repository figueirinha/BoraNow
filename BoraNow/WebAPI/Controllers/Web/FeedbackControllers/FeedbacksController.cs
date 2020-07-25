using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Feedbacks;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Users;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Feedbacks;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.HtmlComponents;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Users;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Support;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Web.FeedbackControllers
{
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class FeedbacksController : Controller
    {
        private readonly FeedbackBusinessObject _bo = new FeedbackBusinessObject();
        private readonly VisitorBusinessObject _vbo = new VisitorBusinessObject();
        private readonly InterestPointBusinessObject _ipbo = new InterestPointBusinessObject();

        private string GetDeleteRef()
        {
            return this.ControllerContext.RouteData.Values["controller"] + "/" + nameof(Delete);
        }

        private List<BreadCrumb> GetCrumbs()
        {
            return new List<BreadCrumb>()
                { new BreadCrumb(){Icon ="fa-home", Action="Index", Controller="Home", Text="Home"},
                  new BreadCrumb(){Icon = "fa-user-cog", Action="Administration", Controller="Home", Text = "Administration"},
                  new BreadCrumb(){Icon = "fas fa-comments", Action="Index", Controller="Feedbacks", Text = "Feedbacks"}
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
            var vListOperation = await _vbo.ListAsync();
            if (!vListOperation.Success) return OperationErrorBackToIndex(listOperation.Exception);
            var ipListOperation = await _ipbo.ListAsync();
            if (!ipListOperation.Success) return OperationErrorBackToIndex(listOperation.Exception);

            var list = new List<FeedbackViewModel>();
            foreach (var item in listOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    list.Add(FeedbackViewModel.Parse(item));
                }
            }

            var vList = new List<VisitorViewModel>();
            foreach (var item in vListOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    vList.Add(VisitorViewModel.Parse(item));
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
            ViewData["Title"] = "Feedbacks";
            ViewData["BreadCrumbs"] = GetCrumbs();
            ViewData["DeleteHref"] = GetDeleteRef();
            ViewBag.Visitors = vList;
            ViewBag.InterestPoints = ipList;
            return View(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return RecordNotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
            if (getOperation.Result == null) return RecordNotFound();

            var getIPOperation = await _ipbo.ReadAsync(getOperation.Result.InterestPointId);
            if (!getIPOperation.Success) return OperationErrorBackToIndex(getIPOperation.Exception);
            if (getIPOperation.Result == null) return RecordNotFound();

            var getVOperation = await _vbo.ReadAsync(getOperation.Result.VisitorId);
            if (!getVOperation.Success) return OperationErrorBackToIndex(getVOperation.Exception);
            if (getVOperation.Result == null) return RecordNotFound();

            var vm = FeedbackViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Feedbacks details";

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "Feedbacks", Icon = "fa-search", Text = "Detail" });
            ViewData["InterestPoints"] = InterestPointViewModel.Parse(getIPOperation.Result);
            ViewData["Visitors"] = VisitorViewModel.Parse(getVOperation.Result);
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }

        [HttpGet("new")]
        public async Task<IActionResult> Create()
        {
            var vListOperation = await _vbo.ListAsync();
            if (!vListOperation.Success) return OperationErrorBackToIndex(vListOperation.Exception);
            var vList = new List<VisitorViewModel>();
            foreach (var n in vListOperation.Result)
            {
                if (!n.IsDeleted)
                {
                    var nvm = VisitorViewModel.Parse(n);
                    vList.Add(nvm);
                }
                ViewBag.Visitors = vList.Select(ip => new SelectListItem() { Text = ip.FirstName, Value = ip.Id.ToString() });
            }

            var ipListOperation = await _ipbo.ListAsync();
            if (!ipListOperation.Success) return OperationErrorBackToIndex(vListOperation.Exception);
            var ipList = new List<InterestPointViewModel>();
            foreach (var ip in ipListOperation.Result)
            {
                if (!ip.IsDeleted)
                {
                    var ipvm = InterestPointViewModel.Parse(ip);
                    ipList.Add(ipvm);
                }
                ViewBag.InterestPoints = ipList.Select(ip => new SelectListItem() { Text = ip.Name, Value = ip.Id.ToString() });
            }
            ViewData["Title"] = "New Feedback";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "Feedbacks", Icon = "fa-plus", Text = "New" });
            ViewData["BreadCrumbs"] = crumbs;
            return View();
        }

        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description, Stars, Date, InterestPointId, VisitorId")] FeedbackViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var Feedback = vm.ToFeedback();
                var createOperation = await _bo.CreateAsync(Feedback);
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

            var vm = FeedbackViewModel.Parse(getOperation.Result);
            var listVOperation = await _vbo.ListAsync();
            if (!listVOperation.Success) return OperationErrorBackToIndex(listVOperation.Exception);

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
            ViewBag.Visitors = vList;

            var listIpOperation = await _ipbo.ListAsync();
            if (!listIpOperation.Success) return OperationErrorBackToIndex(listIpOperation.Exception);

            var ipList = new List<SelectListItem>();
            foreach (var item in listIpOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Name };
                    if (item.Id == vm.VisitorId) listItem.Selected = true;
                    ipList.Add(listItem);
                }
            }
            ViewBag.InterestPoints = ipList;

            ViewData["Title"] = "Edit Feedback";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Edit", Controller = "Feedbacks", Icon = "fa-edit", Text = "Edit" });
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id, Description, Stars, Date, InterestPointId, VisitorId")] FeedbackViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var getOperation = await _bo.ReadAsync((Guid)id);
                if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
                if (getOperation.Result == null) return NotFound();
                var result = getOperation.Result;
                result.Description = vm.Description;
                result.Stars = vm.Stars;
                result.Date = vm.Date;
                result.InterestPointId = vm.InterestPointId;
                result.VisitorId = vm.VisitorId;
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
            if (id == null) return NotFound();
            var deleteOperation = await _bo.DeleteAsync((Guid)id);
            if (!deleteOperation.Success) return OperationErrorBackToIndex(deleteOperation.Exception);
            return RedirectToAction(nameof(Index));
        }
    }
}
