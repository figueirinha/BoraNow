using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Users;
using Recodme.RD.BoraNow.DataLayer.Users;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Users;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private RoleBusinessObject _bo = new RoleBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody] RoleViewModel vm)
        {
            var role = new Role(vm.Name);

            var res = _bo.Create(role);
            if (!res.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            else return new ObjectResult(HttpStatusCode.OK);
        }

        [HttpGet("{Id}")]
        public ActionResult<RoleViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var vm = RoleViewModel.Parse(res.Result);
                return vm;
            }
            else return new ObjectResult(HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public ActionResult<List<RoleViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var list = new List<RoleViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(RoleViewModel.Parse(item));
            }
            return list;
        }

        [HttpPost]
        public ActionResult Update([FromBody] RoleViewModel vm)
        {
            var currentResult = _bo.Read(vm.Id);
            if (!currentResult.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var current = currentResult.Result;
            if (current == null) return NotFound();
            if (current.Name == vm.Name) return new ObjectResult(HttpStatusCode.NotModified);

            if (current.Name != vm.Name) current.Name = vm.Name;

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