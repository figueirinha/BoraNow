using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Users;
using Recodme.RD.BoraNow.DataLayer.Users;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Users;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Api.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private VisitorBusinessObject _bo = new VisitorBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody] VisitorViewModel vm)
        {
            var visitor = new Visitor(vm.FirstName, vm.LastName, vm.BirthDate, vm.Gender, vm.ProfileId);

            var res = _bo.Create(visitor);
            if (!res.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            else return new ObjectResult(HttpStatusCode.OK);
        }

        [HttpGet("{Id}")]
        public ActionResult<VisitorViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var vm = VisitorViewModel.Parse(res.Result);
                return vm;
            }
            else return new ObjectResult(HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public ActionResult<List<VisitorViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var list = new List<VisitorViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(VisitorViewModel.Parse(item));
            }
            return list;
        }

        [HttpPost]
        public ActionResult Update([FromBody] VisitorViewModel vm)
        {
            var currentResult = _bo.Read(vm.Id);
            if (!currentResult.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var current = currentResult.Result;
            if (current == null) return NotFound();
            if (current.FirstName == vm.FirstName && current.LastName == vm.LastName
                && current.BirthDate == vm.BirthDate
                && current.Gender == vm.Gender
                && current.ProfileId == vm.ProfileId) return new ObjectResult(HttpStatusCode.NotModified);

            if (current.FirstName != vm.FirstName) current.FirstName = vm.FirstName;
            if (current.LastName != vm.LastName) current.LastName = vm.LastName;
            if (current.BirthDate != vm.BirthDate) current.BirthDate = vm.BirthDate;
            if (current.Gender != vm.Gender) current.Gender = vm.Gender;
            if (current.ProfileId != vm.ProfileId) current.ProfileId = vm.ProfileId;

            var updateResult = _bo.Update(current);
            if (!updateResult.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public ActionResult Delete(Guid id)
        {
            var result = _bo.Delete(id);
            if (result.Success) return Ok();
            return new ObjectResult(HttpStatusCode.InternalServerError);
        }
    }
}