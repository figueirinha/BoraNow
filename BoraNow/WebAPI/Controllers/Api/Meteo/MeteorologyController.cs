using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Meteo;
using Recodme.RD.BoraNow.DataLayer.Meteo;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Meteo;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Api.Meteo
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeteorologyController : ControllerBase
    {
        private MeteorologyBusinessObject _bo = new MeteorologyBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody] MeteorologyViewModel mvm)
        {
            var meteo = new Meteorology(mvm.MaxTemperature, mvm.MinTemperature, mvm.RainPercentage, mvm.UvIndex, mvm.WindIndex, mvm.Date);

            var res = _bo.Create(meteo);
            var code = res.Success ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
            return new ObjectResult(code);
        }

        [HttpGet("{Id}")]
        public ActionResult<MeteorologyViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var mvm = MeteorologyViewModel.Parse(res.Result);
                return mvm;
            }
            else return new ObjectResult(HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public ActionResult<List<MeteorologyViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var list = new List<MeteorologyViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(MeteorologyViewModel.Parse(item));
            }
            return list;
        }

        [HttpPut]
        public ActionResult Update([FromBody] MeteorologyViewModel mvm)
        {
            var currentResult = _bo.Read(mvm.Id);
            if (!currentResult.Success) return new ObjectResult(HttpStatusCode.InternalServerError);
            var current = currentResult.Result;
            if (current == null) return NotFound();
            if (current.MaxTemperature == mvm.MaxTemperature && current.MinTemperature == mvm.MinTemperature
                && current.RainPercentage == mvm.RainPercentage && current.UvIndex == mvm.UvIndex
                && current.WindIndex == mvm.WindIndex && current.Date == mvm.Date) return new ObjectResult(HttpStatusCode.NotModified);

            if (current.MaxTemperature != mvm.MaxTemperature) current.MaxTemperature = mvm.MaxTemperature;
            if (current.MinTemperature != mvm.MinTemperature) current.MinTemperature = mvm.MinTemperature;
            if (current.RainPercentage != mvm.RainPercentage) current.RainPercentage = mvm.RainPercentage;
            if (current.UvIndex != mvm.UvIndex) current.UvIndex = mvm.UvIndex;
            if (current.WindIndex != mvm.WindIndex) current.WindIndex = mvm.WindIndex;
            if (current.Date != mvm.Date) current.Date = mvm.Date;

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