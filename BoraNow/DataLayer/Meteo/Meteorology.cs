using Recodme.RD.BoraNow.DataLayer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Recodme.RD.BoraNow.DataLayer.Meteo
{
    public class Meteorology : Entity
    {
        private double _maxTemperature;
        public double MaxTemperature
        {
            get
            {
                return _maxTemperature;
            }
            set
            {
                _maxTemperature = value;
                RegisterChange();
            }
        }

        private double _minTemperature;
        public double MinTemperature
        {
            get
            {
                return _minTemperature;
            }
            set
            {
                _minTemperature = value;
                RegisterChange();
            }
        }

        private int _rainPercentage;

        [Range(0, 100)]
        public int RainPercentage
        {
            get
            {
                return _rainPercentage;
            }
            set
            {
                _rainPercentage = value;
                RegisterChange();
            }
        }

        private int _uvIndex;

        [Range(1, 11)]
        public int UvIndex
        {
            get
            {
                return _uvIndex;
            }
            set
            {
                _uvIndex = value;
                RegisterChange();
            }
        }

        private int _windIndex;

        [Range(0, 12)]
        public int WindIndex
        {
            get
            {
                return _windIndex;
            }
            set
            {
                _windIndex = value;
                RegisterChange();
            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                RegisterChange();
            }
        }
        public Meteorology(double maxTemperature, double minTemperature, int rainPercentage, int uvIndex, int windIndex, DateTime date) : base()
        {
            _maxTemperature = maxTemperature;
            _minTemperature = minTemperature;
            _rainPercentage = rainPercentage;
            _uvIndex = uvIndex;
            _windIndex = windIndex;
            _date = date;
        }

        public Meteorology(Guid id, DateTime createAt, DateTime updateAt, bool isDeleted, double maxTemperature, double minTemperature, int rainPercentage, int uvIndex, int windIndex, DateTime date) : base(id, createAt, updateAt, isDeleted)
        {
            _maxTemperature = maxTemperature;
            _minTemperature = minTemperature;
            _rainPercentage = rainPercentage;
            _uvIndex = uvIndex;
            _windIndex = windIndex;
            _date = date;
        }
    }
}
