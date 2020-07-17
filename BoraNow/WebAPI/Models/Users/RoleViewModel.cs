using Recodme.RD.BoraNow.DataLayer.Users;
using System;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Users
{
    public class RoleViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public Role ToRole()
        {
            return new Role(Name);
        }

        public static RoleViewModel Parse(Role role)
        {
            return new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}
