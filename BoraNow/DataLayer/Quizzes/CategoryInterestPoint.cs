using Recodme.RD.BoraNow.DataLayer.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recodme.RD.BoraNow.DataLayer.Quizzes
{
    public class CategoryInterestPoint : NamedEntity
    {
        public virtual ICollection<InterestPointCategoryInterestPoint> CategoryInterestPoints { get; set; }

        public CategoryInterestPoint(string name) : base(name)
        {
        }

        public CategoryInterestPoint(Guid id, DateTime createAt, DateTime updateAt, bool isDeleted, string name) : base(id, createAt, updateAt, isDeleted, name)
        {
        }
    }
}
