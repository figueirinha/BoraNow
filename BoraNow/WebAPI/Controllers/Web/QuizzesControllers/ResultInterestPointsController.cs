using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes;
using WebAPI.Models;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Web.QuizzesControllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ResultInterestPointsController : Controller
    {
        private readonly ResultInterestPointBusinessObject _ripbo = new ResultInterestPointBusinessObject();
        private readonly InterestPointBusinessObject _ipbo = new InterestPointBusinessObject();
        private readonly ResultBusinessObject _rbo = new ResultBusinessObject();
        public async Task<IActionResult> Index()
        {
            var listOperation = await _ripbo.ListAsync();
            if (!listOperation.Success) return View("Error", new ErrorViewModel() { RequestId = listOperation.Exception.Message });

            var interestPointListOperation = await _ipbo.ListAsync();
            if (!interestPointListOperation.Success) return View("Error", new ErrorViewModel() { RequestId = interestPointListOperation.Exception.Message });

            var resultListOperation = await _rbo.ListAsync();
            if (!resultListOperation.Success) return View("Error", new ErrorViewModel() { RequestId = resultListOperation.Exception.Message });


            var resultInterestPointList = new List<ResultInterestPointViewModel>();
            foreach (var item in listOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    resultInterestPointList.Add(ResultInterestPointViewModel.Parse(item));
                }
            }
            var interestPointList = new List<InterestPointViewModel>();
            foreach (var item in interestPointListOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    interestPointList.Add(InterestPointViewModel.Parse(item));
                }
            }
            var resultList = new List<ResultViewModel>();
            foreach (var item in resultListOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    resultList.Add(ResultViewModel.Parse(item));
                }
            }
           
            ViewBag.InterestPoints = interestPointList;
            ViewBag.Results = resultList;
            return View(resultList);
        }
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var getOperation = await _ripbo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
            if (getOperation.Result == null) return NotFound();
            var vm = ResultInterestPointViewModel.Parse(getOperation.Result);
            return View(vm);

        }
        public async Task<IActionResult> Create()
        {
            var interestPointListOperation = await _ipbo.ListAsync();
            if (!interestPointListOperation.Success) return View("Error", new ErrorViewModel() { RequestId = "Error" });
            var interestPointList = new List<InterestPointViewModel>();
            foreach (var interestPoint in interestPointListOperation.Result)
            {
                if (!interestPoint.IsDeleted)
                {
                    var interestpointVm = InterestPointViewModel.Parse(interestPoint);
                    interestPointList.Add(interestpointVm);
                }
                ViewBag.InterestPoints = interestPointList.Select(interestPoint => new SelectListItem() { Text = interestPoint.Name, Value = interestPoint.Id.ToString() });
            }
            var resultListOperation = await _rbo.ListAsync();
            if (!resultListOperation.Success) return View("Error", new ErrorViewModel() { RequestId = "Error" });
            var resultList = new List<ResultViewModel>();
            foreach (var result in resultListOperation.Result)
            {
                if (!result.IsDeleted)
                {
                    var resultVm = ResultViewModel.Parse(result);
                    resultList.Add(resultVm);
                }
                ViewBag.Visitors = resultList.Select(result => new SelectListItem() { Text = result.Title, Value = result.Id.ToString() });
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InterestPointId", "ResultId")] ResultInterestPointViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var resultInterestPoint = vm.ToResultInterestPoint();
                var createOperation = await _ripbo.CreateAsync(resultInterestPoint);
                if (!createOperation.Success) return View("Error", new ErrorViewModel() { RequestId = createOperation.Exception.Message });
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null) return NotFound();
        //    var getOperation = await _ripbo.ReadAsync((Guid)id);
        //    if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
        //    if (getOperation.Result == null) return NotFound();
        //    var vm = ResultInterestPointViewModel.Parse(getOperation.Result);
        //    return View(vm);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("Id")] ResultInterestPointViewModel vm)
        //{
            //if (ModelState.IsValid)
            //{
            //    var getOperation = await _ripbo.ReadAsync((Guid)id);
            //    if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
            //    if (getOperation.Result == null) return NotFound();
            //    var result = getOperation.Result;
            //    result.Title = vm.Title;
            //    result.Date = vm.Date;
            //    var updateOperation = await _rbo.UpdateAsync(result);
            //    if (!updateOperation.Success) return View("Error", new ErrorViewModel() { RequestId = updateOperation.Exception.Message });
            //}
        //    return RedirectToAction(nameof(Index));
        //}
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var deleteOperation = await _ripbo.DeleteAsync((Guid)id);
            if (!deleteOperation.Success) return View("Error", new ErrorViewModel() { RequestId = deleteOperation.Exception.Message });
            return RedirectToAction(nameof(Index));
        }
    }
}

