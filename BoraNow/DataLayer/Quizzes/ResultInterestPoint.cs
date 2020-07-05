using Recodme.RD.BoraNow.DataLayer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.RD.BoraNow.DataLayer.Quizzes
{
    public class ResultInterestPoint : Entity
    {
        [ForeignKey("Result")]
        public Guid ResultId { get; set; }
        public virtual Result Result { get; set; }

        [ForeignKey("InterestPoint")]
        public Guid InterestPointId { get; set; }
        public virtual InterestPoint InterestPoint { get; set; }

        public ResultInterestPoint(Guid resultId, Guid interestPointId) : base()
        {
            ResultId = resultId;
            InterestPointId = interestPointId;
        }

        public ResultInterestPoint(Guid id, DateTime createAt, DateTime updateAt, bool isDeleted, Guid resultId, Guid interestPointId) : base(id, createAt, updateAt, isDeleted)
        {
            ResultId = resultId;
            InterestPointId = interestPointId;
        }
    }
}
