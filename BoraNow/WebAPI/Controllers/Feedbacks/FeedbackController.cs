using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Feedbacks;
using Recodme.RD.BoraNow.DataLayer.Feedbacks;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Feedbacks;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Feedbacks
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private FeedbackBusinessObject _bo =  new FeedbackBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody] FeedbackViewModel fvm)
        {
            var feedback = new Feedback(fvm.Description, fvm.Stars, fvm.Date, fvm.InterestPointId);

            var res = _bo.Create(feedback);
            if (!res.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            else return new ObjectResult(HttpStatusCode.OK);
        }

        [HttpGet("{Id}")]
        public ActionResult<FeedbackViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var fmv = FeedbackViewModel.Parse(res.Result);
                return fmv;
            }
            else return new ObjectResult(HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public ActionResult<List<FeedbackViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var list = new List<FeedbackViewModel>();
            foreach(var item in res.Result)
            {
                list.Add(FeedbackViewModel.Parse(item));
            }
            return list;
        }

        [HttpPost]
        public ActionResult Update([FromBody] FeedbackViewModel fvm)
        {
            var currentResult = _bo.Read(fvm.Id);
            if (!currentResult.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var current = currentResult.Result;
            if (current == null) return NotFound();
            if (current.Description == fvm.Description && current.Stars == fvm.Stars
                && current.Date == fvm.Date && current.InterestPointId == fvm.InterestPointId) return new ObjectResult(HttpStatusCode.NotModified);

            if (current.Description != fvm.Description) current.Description = fvm.Description;
            if (current.Stars != fvm.Stars) current.Stars = fvm.Stars;
            if (current.Date != fvm.Date) current.Date = fvm.Date;
            if (current.InterestPointId != fvm.InterestPointId) current.InterestPointId = fvm.InterestPointId;

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