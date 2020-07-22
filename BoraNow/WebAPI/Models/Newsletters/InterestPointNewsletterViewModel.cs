using Recodme.RD.BoraNow.DataLayer.Newsletters;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;


namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Newsletters
{
    public class InterestPointNewsletterViewModel
    {
        public Guid Id { get; set; }
        public Guid InterestPointId { get; set;  }
        public Guid NewsLetterId { get; set; }


        //public Newsletter Newsletter { get; set; }
        //public InterestPoint InterestPoint { get; set; }

        public InterestPointNewsletter ToInterestPointNewsletter()
        {
            return new InterestPointNewsletter(InterestPointId, NewsLetterId);
        }

        public static InterestPointNewsletterViewModel Parse(InterestPointNewsletter interestPointNewsletter)
        {
            return new InterestPointNewsletterViewModel()
            {
                Id = interestPointNewsletter.Id,
                InterestPointId = interestPointNewsletter.InterestPointId,
                NewsLetterId = interestPointNewsletter.NewsLetterId
            };
        }
    }
}