//using System;
//using System.Collections.Generic;
//using System.Net;
//using Microsoft.AspNetCore.Mvc;
//using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Users;
//using Recodme.RD.BoraNow.DataLayer.Users;
//using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Users;

//namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Api.Users
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProfileController : ControllerBase
//    {
//        private ProfileBusinessObject _bo = new ProfileBusinessObject();

//        [HttpPost]
//        public ActionResult Create([FromBody] ProfileViewModel vm)
//        {
//            var profile = new Profile(vm.Description, vm.PhotoPath, vm.CountryId, vm.VisitorId, vm.CompanyId, vm.UserId);

//            var res = _bo.Create(profile);
//            if (!res.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
//            else return new ObjectResult(HttpStatusCode.OK);
//        }

//        [HttpGet("{Id}")]
//        public ActionResult<ProfileViewModel> Get(Guid id)
//        {
//            var res = _bo.Read(id);
//            if (res.Success)
//            {
//                if (res.Result == null) return NotFound();
//                var vm = ProfileViewModel.Parse(res.Result);
//                return vm;
//            }
//            else return new ObjectResult(HttpStatusCode.InternalServerError);
//        }

//        [HttpGet]
//        public ActionResult<List<ProfileViewModel>> List()
//        {
//            var res = _bo.List();
//            if (!res.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
//            var list = new List<ProfileViewModel>();
//            foreach (var item in res.Result)
//            {
//                list.Add(ProfileViewModel.Parse(item));
//            }
//            return list;
//        }

//        [HttpPost]
//        public ActionResult Update([FromBody] ProfileViewModel vm)
//        {
//            var currentResult = _bo.Read(vm.Id);
//            if (!currentResult.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
//            var current = currentResult.Result;
//            if (current == null) return NotFound();
//            if (current.Description == vm.Description && current.PhotoPath == vm.PhotoPath
//                && current.CountryId == vm.CountryId 
//                && current.UserId == vm.UserId 
//                && current.VisitorId == vm.VisitorId && current.CompanyId == vm.CompanyId) return new ObjectResult(HttpStatusCode.NotModified);

//            if (current.Description != vm.Description) current.Description = vm.Description;
//            if (current.PhotoPath != vm.PhotoPath) current.PhotoPath = vm.PhotoPath;
//            if (current.CountryId != vm.CountryId) current.CountryId = vm.CountryId;
//            if (current.VisitorId != vm.VisitorId) current.VisitorId = vm.VisitorId;
//            if (current.CompanyId != vm.CompanyId) current.CompanyId = vm.CompanyId;
//            if (current.UserId != vm.UserId) current.UserId = vm.UserId;

//            var updateResult = _bo.Update(current);
//            if (!updateResult.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
//            return Ok();
//        }

//        [HttpDelete("{Id}")]
//        public ActionResult Delete(Guid id)
//        {
//            var result = _bo.Delete(id);
//            if (result.Success) return Ok();
//            return new ObjectResult(HttpStatusCode.InternalServerError);
//        }
//    }
//}