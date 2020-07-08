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
    public class QuizQuestionController : ControllerBase
    {
        private QuizQuestionBusinessObject _bo = new QuizQuestionBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody] QuizQuestionViewModel vm)
        {
            var c = new QuizQuestion(vm.Question, vm.QuizId);

            var res = _bo.Create(c);
            var code = res.Success ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
            return new ObjectResult(code);
        }

        [HttpGet("{id}")]
        public ActionResult<QuizQuestionViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var cvm = QuizQuestionViewModel.Parse(res.Result);
                return cvm;
            }
            else return new ObjectResult(HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public ActionResult<List<QuizQuestionViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var list = new List<QuizQuestionViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(QuizQuestionViewModel.Parse(item));
            }
            return list;
        }

        [HttpPut]
        public ActionResult Update([FromBody] QuizQuestionViewModel c)
        {
            var currentResult = _bo.Read(c.Id);
            if (!currentResult.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var current = currentResult.Result;
            if (current == null) return NotFound();
            if (current.Question == c.Question && current.QuizId == c.QuizId) return new ObjectResult(HttpStatusCode.NotModified);

            if (current.Question != c.Question) current.Question = c.Question;
            if (current.QuizId != c.QuizId) current.QuizId = c.QuizId;
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
