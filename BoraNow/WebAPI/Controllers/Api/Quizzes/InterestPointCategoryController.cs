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

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Api.Quizzes
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestPointCategoryController : ControllerBase
    {
        private InterestPointCategoryBusinessObject _bo = new InterestPointCategoryBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody] InterestPointCategoryViewModel vm)
        {
            var c = new InterestPointCategory(vm.InterestPointId, vm.CategoryId);

            var res = _bo.Create(c);
            var code = res.Success ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
            return new ObjectResult(code);
        }

        [HttpGet("{id}")]
        public ActionResult<InterestPointCategoryViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var cvm = InterestPointCategoryViewModel.Parse(res.Result);
                return cvm;
            }
            else return new ObjectResult(HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public ActionResult<List<InterestPointCategoryViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var list = new List<InterestPointCategoryViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(InterestPointCategoryViewModel.Parse(item));
            }
            return list;
        }

        [HttpPut]
        public ActionResult Update([FromBody] InterestPointCategoryViewModel c)
        {
            var currentResult = _bo.Read(c.Id);
            if (!currentResult.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var current = currentResult.Result;
            if (current == null) return NotFound();
            if (current.CategoryId == c.CategoryId && current.InterestPointId == c.InterestPointId) return new ObjectResult(HttpStatusCode.NotModified);

            if (current.CategoryId != c.CategoryId) current.CategoryId = c.CategoryId;
            if (current.InterestPointId != c.InterestPointId) current.InterestPointId = c.InterestPointId;
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