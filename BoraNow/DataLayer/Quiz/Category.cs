using Recodme.RD.BoraNow.DataLayer.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recodme.RD.BoraNow.DataLayer.Quiz
{
    public class Category : NamedEntity
    {
        public virtual ICollection<InterestPointCategory> CategoryInterestPoints { get; set; }
        public virtual ICollection<CategoryQuizAnswer> CategoryQuizAnswers { get; set; }

        public Category(string name) : base(name)
        {
        }

        public Category(Guid id, DateTime createAt, DateTime updateAt, bool isDeleted, string name) : base(id, createAt, updateAt, isDeleted, name)
        {
        }
    }
}
