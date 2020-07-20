using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Api.Quizzes
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryQuizAnswerController : ControllerBase
    {
        private CategoryQuizAnswerBusinessObject _bo = new CategoryQuizAnswerBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody] CategoryQuizAnswerViewModel vm)
        {
            var c = new CategoryQuizAnswer(vm.CategoryId, vm.QuizAnswerId);

            var res = _bo.Create(c);
            var code = res.Success ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
            return new ObjectResult(code);
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryQuizAnswerViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var cvm = CategoryQuizAnswerViewModel.Parse(res.Result);
                return cvm;
            }
            else return new ObjectResult(HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public ActionResult<List<CategoryQuizAnswerViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var list = new List<CategoryQuizAnswerViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(CategoryQuizAnswerViewModel.Parse(item));
            }
            return list;
        }

        [HttpPut]
        public ActionResult Update([FromBody] CategoryQuizAnswerViewModel c)
        {
            var currentResult = _bo.Read(c.Id);
            if (!currentResult.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var current = currentResult.Result;
            if (current == null) return NotFound();
            if (current.CategoryId == c.CategoryId && current.QuizAnswerId == c.QuizAnswerId) return new ObjectResult(HttpStatusCode.NotModified);

            if (current.CategoryId != c.CategoryId) current.CategoryId = c.CategoryId;
            if (current.QuizAnswerId != c.QuizAnswerId) current.QuizAnswerId = c.QuizAnswerId;
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
