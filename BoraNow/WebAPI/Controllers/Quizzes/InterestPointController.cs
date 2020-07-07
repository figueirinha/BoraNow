using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Quizzes
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestPointController : ControllerBase
    {
        private InterestPointBusinessObject _bo = new InterestPointBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody] InterestPointViewModel vm)
        {
            var c = new InterestPoint(vm.Name, vm.Description, vm.Address, vm.PhotoPath, vm.OpeningHours,
            vm.ClosingHours, vm.ClosingDays, vm.CovidSafe, vm.Status);

            var res = _bo.Create(c);
            var code = res.Success ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
            return new ObjectResult(code);
        }

        [HttpGet("{id}")]
        public ActionResult<InterestPointViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var cvm = InterestPointViewModel.Parse(res.Result);
                return cvm;
            }
            else return new ObjectResult(HttpStatusCode.InternalServerError);
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<InterestPointViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var list = new List<InterestPointViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(InterestPointViewModel.Parse(item));
            }
            return list;
        }

        [HttpPut]
        public ActionResult Update([FromBody] InterestPointViewModel c)
        {
            var currentResult = _bo.Read(c.Id);
            if (!currentResult.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var current = currentResult.Result;
            if (current == null) return NotFound();
            if (current.Name == c.Name && current.Description == c.Description
                && current.Address == c.Address && current.PhotoPath == c.PhotoPath
                && current.OpeningHours == c.OpeningHours && current.ClosingHours == c.ClosingHours
                && current.ClosingDays == c.ClosingDays && current.CovidSafe == c.CovidSafe
                && current.Status == c.Status) return new ObjectResult(HttpStatusCode.NotModified);

            if (current.Address != c.Address) current.Address = c.Address;
            if (current.Name != c.Name) current.Name = c.Name;
            if (current.Description != c.Description) current.Description = c.Description;
            if (current.PhotoPath != c.PhotoPath) current.PhotoPath = c.PhotoPath;
            if (current.OpeningHours != c.OpeningHours) current.OpeningHours = c.OpeningHours;
            if (current.ClosingHours != c.ClosingHours) current.ClosingHours = c.ClosingHours;
            if (current.ClosingDays != c.ClosingDays) current.ClosingDays = c.ClosingDays;
            if (current.CovidSafe != c.CovidSafe) current.CovidSafe = c.CovidSafe;
            if (current.Status != c.Status) current.Status = c.Status;
            var updateResult = _bo.Update(current);
            if (!updateResult.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var result = _bo.Delete(id);
            if (result.Success) return Ok();
            return new ObjectResult(HttpStatusCode.InternalServerError);
        }
    }
}