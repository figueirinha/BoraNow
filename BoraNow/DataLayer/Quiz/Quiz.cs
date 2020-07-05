using Recodme.RD.BoraNow.DataLayer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Recodme.RD.BoraNow.DataLayer.Quiz
{
    public class Quiz : Entity
    {
        private string _title;

        [Required]
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                RegisterChange();
            }
        }

        public virtual ICollection<Result> Results { get; set; }
        public virtual ICollection<QuizQuestion> QuizQuestions { get; set; }

        public Quiz(string title) : base()
        {
            _title = title;
        }

        public Quiz(Guid id, DateTime createAt, DateTime updateAt, bool isDeleted, string title) : base(id, createAt, updateAt, isDeleted)
        {
            _title = title;
        }
    }
}
