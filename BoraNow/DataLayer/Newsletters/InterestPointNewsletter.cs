using Recodme.RD.BoraNow.DataLayer.Base;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.RD.BoraNow.DataLayer.Newsletters
{
    public class InterestPointNewsletter : Entity
    {

        [ForeignKey("InterestPoint")]
        public Guid InterestPointId { get; set; }

        [ForeignKey("NewsLetter")]
        public Guid NewsLetterId { get; set; }

        public virtual InterestPoint InterestPoint { get; set; }
        public virtual Newsletter Newsletter { get; set; }
        public InterestPointNewsletter(Guid interestPointId, Guid newsLetterId): base()
        {
            InterestPointId = interestPointId;
            NewsLetterId = newsLetterId;
        }

        public InterestPointNewsletter(Guid id, DateTime createAt, DateTime updateAt, bool isDeleted, Guid interestPointId, Guid newsLetterId) : base(id, createAt, updateAt, isDeleted)
        {
            InterestPointId = interestPointId;
            NewsLetterId = newsLetterId;

        }
    }
}
