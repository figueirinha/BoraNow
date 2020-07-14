using Recodme.RD.BoraNow.DataLayer.Meteo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Meteo
{
    public class MeteorologyViewModel
    {
        public Guid Id { get; set; }
        public double MaxTemperature { get; set; }
        public double MinTemperature { get; set; }
        public int RainPercentage { get; set; }
        public int UvIndex { get; set; }
        public int WindIndex { get; set; }
        public DateTime Date { get; set; }

        public Meteorology ToMeteorology()
        {
            return new Meteorology(MaxTemperature, MinTemperature, RainPercentage, UvIndex, WindIndex, Date);
        }

        public static MeteorologyViewModel Parse(Meteorology meteo)
        {
            return new MeteorologyViewModel()
            {
                Id = meteo.Id,
                MaxTemperature=meteo.MaxTemperature,
                MinTemperature=meteo.MinTemperature,
                RainPercentage=meteo.RainPercentage,
                UvIndex=meteo.UvIndex,
                WindIndex=meteo.WindIndex,
                Date=meteo.Date
            };
        }

    }
}
