using Recodme.RD.BoraNow.DataLayer.Users;
using System;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Users
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }

        public User ToRole()
        {
            return new User(Email, Password, RoleId);
        }

        public static UserViewModel Parse(User user)
        {
            return new UserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                RoleId = user.RoleId
            };
        }
    }
}
