using Recodme.RD.BoraNow.DataLayer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.RD.BoraNow.DataLayer.Quizzes
{
    public class CategoryQuizAnswer : Entity
    {
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey("QuizAnswer")]
        public Guid QuizAnswerId { get; set; }
        public virtual QuizAnswer QuizAnswser { get; set; }


        public CategoryQuizAnswer(Guid categoryId, Guid quizAnswerId) : base()
        {
            CategoryId = categoryId;
            QuizAnswerId = quizAnswerId;
        }

        public CategoryQuizAnswer(Guid id, DateTime createAt, DateTime updateAt, bool isDeleted, Guid categoryId, Guid quizAnswerId) : base(id, createAt, updateAt, isDeleted)
        {
            CategoryId = categoryId;
            QuizAnswerId = quizAnswerId;
        }
    }
}
