using Recodme.RD.BoraNow.DataLayer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.RD.BoraNow.DataLayer.Quizzes
{
    public class InterestPointCategory : Entity
    {
        [ForeignKey("InterestPoint")]
        public Guid InterestPointId { get; set; }
        public virtual InterestPoint InterestPoint { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public InterestPointCategory(Guid interestPointId, Guid categoryId) : base()
        {
            InterestPointId = interestPointId;
            CategoryId = categoryId;
        }

        public InterestPointCategory(Guid id, DateTime createAt, DateTime updateAt, bool isDeleted, Guid interestPointId, Guid categoryId) : base(id, createAt, updateAt, isDeleted)
        {
            InterestPointId = interestPointId;
            CategoryId = categoryId;
        }
    }
}
