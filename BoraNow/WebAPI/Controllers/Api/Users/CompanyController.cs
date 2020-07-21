//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Users;
//using Recodme.RD.BoraNow.DataLayer.Users;
//using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Users;

//namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Api.Users
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CompanyController : ControllerBase
//    {
//        private CompanyBusinessObject _bo = new CompanyBusinessObject();

//        [HttpPost]
//        public ActionResult Create([FromBody] CompanyViewModel vm)
//        {
//            var company = new Company(vm.Name, vm.Representative, vm.PhoneNumber, vm.VatNumber, vm.ProfileId);

//            var res = _bo.Create(company);
//            if (!res.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
//            else return new ObjectResult(HttpStatusCode.OK);
//        }

//        [HttpGet("{Id}")]
//        public ActionResult<CompanyViewModel> Get(Guid id)
//        {
//            var res = _bo.Read(id);
//            if (res.Success)
//            {
//                if (res.Result == null) return NotFound();
//                var vm = CompanyViewModel.Parse(res.Result);
//                return vm;
//            }
//            else return new ObjectResult(HttpStatusCode.InternalServerError);
//        }

//        [HttpGet]
//        public ActionResult<List<CompanyViewModel>> List()
//        {
//            var res = _bo.List();
//            if (!res.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
//            var list = new List<CompanyViewModel>();
//            foreach (var item in res.Result)
//            {
//                list.Add(CompanyViewModel.Parse(item));
//            }
//            return list;
//        }

//        [HttpPost]
//        public ActionResult Update([FromBody] CompanyViewModel vm)
//        {
//            var currentResult = _bo.Read(vm.Id);
//            if (!currentResult.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
//            var current = currentResult.Result;
//            if (current == null) return NotFound();
//            if (current.Name == vm.Name && current.Representative == vm.Representative
//                && current.ProfileId == vm.ProfileId
//                && current.PhoneNumber == vm.PhoneNumber && current.VatNumber == vm.VatNumber) return new ObjectResult(HttpStatusCode.NotModified);

//            if (current.Name != vm.Name) current.Name = vm.Name;
//            if (current.Representative != vm.Representative) current.Representative = vm.Representative;
//            if (current.PhoneNumber != vm.PhoneNumber) current.PhoneNumber = vm.PhoneNumber;
//            if (current.VatNumber != vm.VatNumber) current.VatNumber = vm.VatNumber;
//            if (current.ProfileId != vm.ProfileId) current.ProfileId = vm.ProfileId;

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