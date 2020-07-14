using Recodme.RD.BoraNow.DataLayer.Base;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.RD.BoraNow.DataLayer.Feedbacks
{
    public class Feedback : Entity
    {
        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                RegisterChange();
            }
        }

        private int _stars;

        [Range(1, 5)]
        public int Stars
        {
            get
            {
                return _stars;
            }
            set
            {
                _stars = value;
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

        [ForeignKey("InterestPoint")]
        public Guid InterestPointId { get; set; }
        public virtual InterestPoint InterestPoint { get; set; }
   
        public Feedback(string description, int stars, DateTime date, Guid interestPointId) : base()
        {
            _description = description;
            _stars = stars;
            _date = date;
            InterestPointId = interestPointId;
        }

        public Feedback(Guid id, DateTime createAt, DateTime updateAt, bool isDeleted, string description, int stars, DateTime date, Guid interestPointId) : base(id, createAt, updateAt, isDeleted)
        {
            _description = description;
            _stars = stars;
            _date = date;
            InterestPointId = interestPointId;
        }
    }
}
