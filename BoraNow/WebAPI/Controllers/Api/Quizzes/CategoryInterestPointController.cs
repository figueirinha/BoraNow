﻿using System;
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
    public class CategoryInterestPointController : ControllerBase
    {
        private CategoryInterestPointBusinessObject _bo = new CategoryInterestPointBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody] CategoryInterestPointViewModel vm)
        {
            var c = new CategoryInterestPoint(vm.Name);

            var res = _bo.Create(c);
            var code = res.Success ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
            return new ObjectResult(code);
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryInterestPointViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var cvm = CategoryInterestPointViewModel.Parse(res.Result);
                return cvm;
            }
            else return new ObjectResult(HttpStatusCode.InternalServerError);
        }

       [HttpGet]
        public ActionResult<List<CategoryInterestPointViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var list = new List<CategoryInterestPointViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(CategoryInterestPointViewModel.Parse(item));
            }
            return list;
        }

        [HttpPut]
        public ActionResult Update([FromBody] CategoryInterestPointViewModel c)
        {
            var currentResult = _bo.Read(c.Id);
            if (!currentResult.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var current = currentResult.Result;
            if (current == null) return NotFound();
            if (current.Name == c.Name) return new ObjectResult(HttpStatusCode.NotModified);

            if (current.Name != c.Name) current.Name = c.Name;
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
