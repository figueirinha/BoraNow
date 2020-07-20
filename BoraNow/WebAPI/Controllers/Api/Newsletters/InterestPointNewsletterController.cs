using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Newsletters;
using Recodme.RD.BoraNow.DataLayer.Newsletters;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Newsletters;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Api.Newsletters
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestPointNewsletterController : ControllerBase
    {
        private InterestPointNewsletterBusinessObject _bo = new InterestPointNewsletterBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody] InterestPointNewsletterViewModel vm)
        {
            var interestPointNewsletter = new InterestPointNewsletter(vm.InterestPointId, vm.NewsLetterId);

            var res = _bo.Create(interestPointNewsletter);
            if (!res.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            else return new ObjectResult(HttpStatusCode.OK);
        }

        [HttpGet("{Id}")]
        public ActionResult<InterestPointNewsletterViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var vm = InterestPointNewsletterViewModel.Parse(res.Result);
                return vm;
            }
            else return new ObjectResult(HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public ActionResult<List<InterestPointNewsletterViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var list = new List<InterestPointNewsletterViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(InterestPointNewsletterViewModel.Parse(item));
            }
            return list;
        }

        [HttpPost]
        public ActionResult Update([FromBody] InterestPointNewsletterViewModel vm)
        {
            var currentResult = _bo.Read(vm.Id);
            if (!currentResult.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var current = currentResult.Result;
            if (current == null) return NotFound();
            if (current.InterestPointId == vm.InterestPointId
                && current.NewsLetterId == vm.NewsLetterId) return new ObjectResult(HttpStatusCode.NotModified);

            if (current.InterestPointId != vm.InterestPointId) current.InterestPointId = vm.InterestPointId;
            if (current.NewsLetterId != vm.NewsLetterId) current.NewsLetterId = vm.NewsLetterId;
          

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