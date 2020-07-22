﻿using System;
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
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Newsletters;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Users;
using WebAPI.Models;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Web.NewsletterControllers
{
    public class InterestPointNewslettersController : Controller
    {
        private readonly InterestPointNewsletterBusinessObject _bo = new InterestPointNewsletterBusinessObject();
        private readonly NewsletterBusinessObject _nbo = new NewsletterBusinessObject();
        private readonly CompanyBusinessObject _cbo = new CompanyBusinessObject();
        private readonly InterestPointBusinessObject _ipbo = new InterestPointBusinessObject();


        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListAsync();
            if (!listOperation.Success) return View("Error", new ErrorViewModel() { RequestId = listOperation.Exception.Message });
            var nListOperation = await _nbo.ListAsync();
            if (!nListOperation.Success) return View("Error", new ErrorViewModel() { RequestId = nListOperation.Exception.Message });
            var cListOperation = await _cbo.ListAsync();
            if (!cListOperation.Success) return View("Error", new ErrorViewModel() { RequestId = cListOperation.Exception.Message });
            var ipListOperation = await _ipbo.ListAsync();
            if (!ipListOperation.Success) return View("Error", new ErrorViewModel() { RequestId = ipListOperation.Exception.Message });

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

            var cList = new List<CompanyViewModel>();
            foreach (var item in cListOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    cList.Add(CompanyViewModel.Parse(item));
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

            ViewBag.Newsletters = nList;
            ViewBag.Companies = cList;
            ViewBag.InterestPoints = ipList;
            return View(list);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
            if (getOperation.Result == null) return NotFound();
            var vm = InterestPointNewsletterViewModel.Parse(getOperation.Result);
            return View(vm);
        }

        public async Task<IActionResult> Create()
        {
            var nListOperation = await _nbo.ListAsync();
            if (!nListOperation.Success) return View("Error", new ErrorViewModel() { RequestId = "Error" });
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

            var cListOperation = await _cbo.ListAsync();
            if (!cListOperation.Success) return View("Error", new ErrorViewModel() { RequestId = "Error" });
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

            var ipListOperation = await _ipbo.ListAsync();
            if (!ipListOperation.Success) return View("Error", new ErrorViewModel() { RequestId = "Error" });
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

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InterestPointId, NewsLetterId")] InterestPointNewsletterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var InterestPointNewsletter = vm.ToInterestPointNewsletter();
                var createOperation = await _bo.CreateAsync(InterestPointNewsletter);
                if (!createOperation.Success) return View("Error", new ErrorViewModel() { RequestId = createOperation.Exception.Message });
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null) return NotFound();
        //    var getOperation = await _bo.ReadAsync((Guid)id);
        //    if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
        //    if (getOperation.Result == null) return NotFound();
        //    var vm = InterestPointNewsletterViewModel.Parse(getOperation.Result);
        //    return View(vm);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("Id, InterestPointId, NewsLetterId")] InterestPointNewsletterViewModel vm)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var getOperation = await _bo.ReadAsync((Guid)id);
        //        if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
        //        if (getOperation.Result == null) return NotFound();
        //        var result = getOperation.Result;
        //        result.InterestPointId = vm.InterestPointId;
        //        result.NewsLetterId = vm.NewsLetterId;        
        //        var updateOperation = await _bo.UpdateAsync(result);
        //        if (!updateOperation.Success) return View("Error", new ErrorViewModel() { RequestId = updateOperation.Exception.Message });
        //    }
        //    return RedirectToAction(nameof(Index));
        //}

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var deleteOperation = await _bo.DeleteAsync((Guid)id);
            if (!deleteOperation.Success) return View("Error", new ErrorViewModel() { RequestId = deleteOperation.Exception.Message });
            return RedirectToAction(nameof(Index));
        }
    }
}