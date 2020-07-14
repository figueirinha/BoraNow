using Recodme.RD.BoraNow.DataLayer.Feedbacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Feedbacks
{
    public class FeedbackViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Stars { get; set; }
        public DateTime Date { get; set; }
        public Guid InterestPointId { get; set; }

        public Feedback ToFeedback()
        {
            return new Feedback(Description, Stars, Date, InterestPointId);
        }

        public static FeedbackViewModel Parse(Feedback fd)
        {
            return new FeedbackViewModel()
            {
                Id = fd.Id,
                Description = fd.Description,
                Stars = fd.Stars,
                Date = fd.Date,
                InterestPointId = fd.InterestPointId
            };
        }
    }
}
