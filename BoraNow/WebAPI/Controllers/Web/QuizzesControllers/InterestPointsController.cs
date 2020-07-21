using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Users;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Users;
using WebAPI.Models;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Web.QuizzesControllers
{
    public class InterestPointsController : Controller
    {
        private readonly InterestPointBusinessObject _bo = new InterestPointBusinessObject();
        private readonly CompanyBusinessObject _cbo = new CompanyBusinessObject();

        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListAsync();
            if (!listOperation.Success) return View("Error", new ErrorViewModel() { RequestId = listOperation.Exception.Message });
            var cListOperation = await _cbo.ListAsync();
            if (!cListOperation.Success) return View("Error", new ErrorViewModel() { RequestId = cListOperation.Exception.Message });

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

            //ViewData["Title"] = "Companies";
            //ViewData["BreadCrumbs"] = new List<string>() { "Home", "Companies" };
            //ViewData["Companies"] = cList;
            ViewBag.Companies = cList;
            return View(list);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
            if (getOperation.Result == null) return NotFound();
            var vm = InterestPointViewModel.Parse(getOperation.Result);
            return View(vm);
        }

        public async Task<IActionResult> Create()
        {
            var cListOperation = await _cbo.ListAsync();
            if (!cListOperation.Success) return View("Error", new ErrorViewModel() { RequestId = "Error" });
            var cList = new List<CompanyViewModel>();
            foreach (var ip in cListOperation.Result)
            {
                if (!ip.IsDeleted)
                {
                    var ipvm = CompanyViewModel.Parse(ip);
                    cList.Add(ipvm);
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
                if (!createOperation.Success) return View("Error", new ErrorViewModel() { RequestId = createOperation.Exception.Message });
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
            if (getOperation.Result == null) return NotFound();
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
                if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
                if (getOperation.Result == null) return NotFound();
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
                if (!updateOperation.Success) return View("Error", new ErrorViewModel() { RequestId = updateOperation.Exception.Message });
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var deleteOperation = await _bo.DeleteAsync((Guid)id);
            if (!deleteOperation.Success) return View("Error", new ErrorViewModel() { RequestId = deleteOperation.Exception.Message });
            return RedirectToAction(nameof(Index));
        }
    }
}