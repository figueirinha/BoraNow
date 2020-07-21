using Recodme.RD.BoraNow.DataLayer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.RD.BoraNow.DataLayer.Quizzes
{
    public class InterestPointCategoryInterestPoint : Entity
    {
        [ForeignKey("InterestPoint")]
        public Guid InterestPointId { get; set; }
        public virtual InterestPoint InterestPoint { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public virtual CategoryInterestPoint Category { get; set; }

        public InterestPointCategoryInterestPoint(Guid interestPointId, Guid categoryId) : base()
        {
            InterestPointId = interestPointId;
            CategoryId = categoryId;
        }

        public InterestPointCategoryInterestPoint(Guid id, DateTime createAt, DateTime updateAt, bool isDeleted, Guid interestPointId, Guid categoryId) : base(id, createAt, updateAt, isDeleted)
        {
            InterestPointId = interestPointId;
            CategoryId = categoryId;
        }
    }
}
