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
    public class NewsletterController : ControllerBase
    {
        private NewsletterBusinessObject _bo = new NewsletterBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody] NewsletterViewModel vm)
        {
            var Newsletter = new Newsletter(vm.Description, vm.Title);

            var res = _bo.Create(Newsletter);
            if (!res.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            else return new ObjectResult(HttpStatusCode.OK);
        }

        [HttpGet("{Id}")]
        public ActionResult<NewsletterViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var vm = NewsletterViewModel.Parse(res.Result);
                return vm;
            }
            else return new ObjectResult(HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public ActionResult<List<NewsletterViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var list = new List<NewsletterViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(NewsletterViewModel.Parse(item));
            }
            return list;
        }

        [HttpPost]
        public ActionResult Update([FromBody] NewsletterViewModel vm)
        {
            var currentResult = _bo.Read(vm.Id);
            if (!currentResult.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var current = currentResult.Result;
            if (current == null) return NotFound();
            if (current.Description == vm.Description && current.Title == vm.Title) return new ObjectResult(HttpStatusCode.NotModified);

            if (current.Description != vm.Description) current.Description = vm.Description;
            if (current.Title != vm.Title) current.Title = vm.Title;

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