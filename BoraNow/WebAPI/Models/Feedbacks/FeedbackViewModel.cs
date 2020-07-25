using Recodme.RD.BoraNow.DataLayer.Feedbacks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Feedbacks
{
    public class FeedbackViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Insert your feedback")]
        public string Description { get; set; }

        [Required]
        public int Stars { get; set; }
        public DateTime Date { get; set; }

        [Display(Name = "Interest Point")]
        public Guid InterestPointId { get; set; }

        [Display(Name = "Visitor")]
        public Guid VisitorId { get; set; }

        public string DateToString
        {
            get
            {
                return $"{Date.Day}-{Date.Month}-{Date.Year}";
            }
        }

        public Feedback ToFeedback()
        {
            return new Feedback(Description, Stars, Date, InterestPointId, VisitorId);
        }

        public static FeedbackViewModel Parse(Feedback fd)
        {
            return new FeedbackViewModel()
            {
                Id = fd.Id,
                Description = fd.Description,
                Stars = fd.Stars,
                Date = fd.Date,
                InterestPointId = fd.InterestPointId,
                VisitorId = fd.VisitorId
            };
        }
    }
}
