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
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.ResultInterestPointzes;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Api.Quizzes
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultInterestPointController : ControllerBase
    {
        private ResultInterestPointBusinessObject _bo = new ResultInterestPointBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody] ResultInterestPointViewModel vm)
        {
            var c = new ResultInterestPoint(vm.ResultId, vm.InterestPointId);

            var res = _bo.Create(c);
            var code = res.Success ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
            return new ObjectResult(code);
        }

        [HttpGet("{id}")]
        public ActionResult<ResultInterestPointViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var cvm = ResultInterestPointViewModel.Parse(res.Result);
                return cvm;
            }
            else return new ObjectResult(HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public ActionResult<List<ResultInterestPointViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var list = new List<ResultInterestPointViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(ResultInterestPointViewModel.Parse(item));
            }
            return list;
        }

        [HttpPut]
        public ActionResult Update([FromBody] ResultInterestPointViewModel c)
        {
            var currentResult = _bo.Read(c.Id);
            if (!currentResult.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var current = currentResult.Result;
            if (current == null) return NotFound();
            if (current.ResultId == c.ResultId && current.InterestPointId == c.InterestPointId) return new ObjectResult(HttpStatusCode.NotModified);

            if (current.ResultId != c.ResultId) current.ResultId = c.ResultId;
            if (current.InterestPointId != c.InterestPointId) current.InterestPointId = c.InterestPointId;
            var updateResultInterestPoint = _bo.Update(current);
            if (!updateResultInterestPoint.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var ResultInterestPoint = _bo.Delete(id);
            if (ResultInterestPoint.Success) return Ok();
            return new ObjectResult(HttpStatusCode.InternalServerError);
        }
    }
}