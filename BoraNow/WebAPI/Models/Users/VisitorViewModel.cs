using Recodme.RD.BoraNow.DataLayer.Users;
using System;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Users
{
    public class VisitorViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        //public Guid ProfileId { get; set; }


        public Visitor ToVisitor()
        {
            return new Visitor(FirstName, LastName, BirthDate, Gender/*, ProfileId*/);
        }

        public static VisitorViewModel Parse(Visitor visitor)
        {
            return new VisitorViewModel()
            {
                Id = visitor.Id,
                FirstName = visitor.FirstName,
                LastName = visitor.LastName,
                BirthDate = visitor.BirthDate,
                Gender = visitor.Gender
                //ProfileId = visitor.ProfileId
            };
        }
    }
}
