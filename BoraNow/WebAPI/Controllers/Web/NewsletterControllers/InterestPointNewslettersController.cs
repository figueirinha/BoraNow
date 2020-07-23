using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Newsletters;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Users;
using Recodme.RD.BoraNow.DataLayer.Newsletters;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.HtmlComponents;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Newsletters;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Users;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Suport;
using WebAPI.Models;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Web.NewsletterControllers
{
    [Route("[controller]")]
    public class InterestPointNewslettersController : Controller
    {
        private readonly InterestPointNewsletterBusinessObject _bo = new InterestPointNewsletterBusinessObject();
        private readonly NewsletterBusinessObject _nbo = new NewsletterBusinessObject();
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
                  new BreadCrumb(){Icon = "far fa-newspaper", Action="Index", Controller="InterestPointNewsletters", Text = "Interest Point Newsletters"}
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
            var nListOperation = await _nbo.ListAsync();
            if (!nListOperation.Success) return OperationErrorBackToIndex(listOperation.Exception);
            var ipListOperation = await _ipbo.ListAsync();
            if (!ipListOperation.Success) return OperationErrorBackToIndex(listOperation.Exception);

            var list = new List<InterestPointNewsletterViewModel>();
            foreach (var item in listOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    list.Add(InterestPointNewsletterViewModel.Parse(item));
                }
            }

            var nList = new List<NewsletterViewModel>();
            foreach (var item in nListOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    nList.Add(NewsletterViewModel.Parse(item));
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
            ViewData["Title"] = "Interest Point Newsletter";
            ViewData["BreadCrumbs"] = GetCrumbs();
            ViewData["DeleteHref"] = GetDeleteRef();
            ViewBag.Newsletters = nList;
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
            var vm = InterestPointNewsletterViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Interest Point Newsletter";

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "InterestPointNewsletters", Icon = "fa-search", Text = "Detail" });

            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }

        [HttpGet("new")]
        public async Task<IActionResult> Create()
        {
            var nListOperation = await _nbo.ListAsync();
            if (!nListOperation.Success) return OperationErrorBackToIndex(nListOperation.Exception);
            var nList = new List<NewsletterViewModel>();
            foreach (var n in nListOperation.Result)
            {
                if (!n.IsDeleted)
                {
                    var nvm = NewsletterViewModel.Parse(n);
                    nList.Add(nvm);
                }
                ViewBag.Newsletters = nList.Select(ip => new SelectListItem() { Text = ip.Title, Value = ip.Id.ToString() });
            }

            var ipListOperation = await _ipbo.ListAsync();
            if (!ipListOperation.Success) return OperationErrorBackToIndex(nListOperation.Exception);
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
            ViewData["Title"] = "New Interest Point Newsletter";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "InterestPointNewsletters", Icon = "fa-plus", Text = "New" });
            ViewData["BreadCrumbs"] = crumbs;
            return View();
        }

        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InterestPointId, NewsLetterId")] InterestPointNewsletterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var InterestPointNewsletter = vm.ToInterestPointNewsletter();
                var createOperation = await _bo.CreateAsync(InterestPointNewsletter);
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
            
            var vm = InterestPointNewsletterViewModel.Parse(getOperation.Result);
            var listNOperation = await _nbo.ListAsync();
            if (!listNOperation.Success) return OperationErrorBackToIndex(listNOperation.Exception);

            var nList = new List<SelectListItem>();
            foreach (var item in listNOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Title };
                    if (item.Id == vm.NewsLetterId) listItem.Selected = true;
                    nList.Add(listItem);
                }
            }
            ViewBag.Newsletters = nList;

            var listIpOperation = await _ipbo.ListAsync();
            if (!listIpOperation.Success) return OperationErrorBackToIndex(listIpOperation.Exception);

            var ipList = new List<SelectListItem>();
            foreach (var item in listIpOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Name };
                    if (item.Id == vm.NewsLetterId) listItem.Selected = true;
                    ipList.Add(listItem);
                }
            }
            ViewBag.InterestPoints = ipList;

            ViewData["Title"] = "Edit Interest Point Newsletter";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Edit", Controller = "InterestPointNewsletters", Icon = "fa-edit", Text = "Edit" });
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id, InterestPointId, NewsLetterId")] InterestPointNewsletterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var getOperation = await _bo.ReadAsync((Guid)id);
                if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
                if (getOperation.Result == null) return NotFound();
                var result = getOperation.Result;
                result.InterestPointId = vm.InterestPointId;
                result.NewsLetterId = vm.NewsLetterId;
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
            if (!deleteOperation.Success) return View("Error", new ErrorViewModel() { RequestId = deleteOperation.Exception.Message });
            return RedirectToAction(nameof(Index));
        }
    }
}
