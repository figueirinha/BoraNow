using Recodme.RD.BoraNow.DataLayer.Newsletters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Newsletters
{
    public class NewsletterViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Please insert a description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please insert a Title")]
        public string Title { get; set; }
        public Newsletter ToNewsletter()
        {
            return new Newsletter(Description, Title);
        }

        public static NewsletterViewModel Parse(Newsletter newsletter)
        {
            return new NewsletterViewModel()
            {
                Id = newsletter.Id,
                Description = newsletter.Description,
                Title = newsletter.Title
            };
        }

    }
}
