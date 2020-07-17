using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Users;
using Recodme.RD.BoraNow.DataLayer.Users;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Users;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserBusinessObject _bo = new UserBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody]UserViewModel vm)
        {
            var user = new User(vm.Email, vm.Password, vm.RoleId);

            var res = _bo.Create(user);
            if (!res.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            else return new ObjectResult(HttpStatusCode.OK);
        }

        [HttpGet("{Id}")]
        public ActionResult<UserViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var vm = UserViewModel.Parse(res.Result);
                return vm;
            }
            else return new ObjectResult(HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public ActionResult<List<UserViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var list = new List<UserViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(UserViewModel.Parse(item));
            }
            return list;
        }

        [HttpPost]
        public ActionResult Update([FromBody]UserViewModel vm)
        {
            var currentResult = _bo.Read(vm.Id);
            if (!currentResult.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var current = currentResult.Result;
            if (current == null) return NotFound();
            if (current.Email == vm.Email && current.Password == vm.Password
                && current.RoleId == vm.RoleId) return new ObjectResult(HttpStatusCode.NotModified);

            if (current.Email != vm.Email) current.Email = vm.Email;
            if (current.Password != vm.Password) current.Password = vm.Password;
            if (current.RoleId != vm.RoleId) current.RoleId = vm.RoleId;
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