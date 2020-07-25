using Microsoft.AspNetCore.Identity;
using Recodme.RD.BoraNow.BusinessLayer.OperationResults;
using Recodme.RD.BoraNow.DataLayer.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Users
{
    public class AccountBusinessController
    {
        private UserManager<User> UserManager { get; set; }
        private RoleManager<Role> RoleManager { get; set; }
        private readonly ProfileBusinessObject _pbo = new ProfileBusinessObject();

        public AccountBusinessController(UserManager<User> uManager, RoleManager<Role> rManager)
        {
            UserManager = uManager;
            RoleManager = rManager;
        }


        TransactionOptions transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromSeconds(30)
        };

        public async Task<OperationResult> Register(string userName, string email, string password, Profile profile, string role)
        {
            if (await UserManager.FindByEmailAsync(email) != null)
                return new OperationResult() { Success = false, Message = $"User {email} already exists" };
            if (await UserManager.FindByNameAsync(userName) != null)
                return new OperationResult() { Success = false, Message = $"User {userName} already exists" };
            using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var createPersonOperation = await _pbo.CreateAsync(profile);
                if (!createPersonOperation.Success)
                {
                    transactionScope.Dispose();
                    return createPersonOperation;
                }
                var user = new User()
                {
                    Email = email,
                    UserName = userName,
                    ProfileId = profile.Id
                };
                var result = await UserManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    transactionScope.Dispose();
                    return new OperationResult() { Success = false, Message = result.ToString() };
                }
                var roleData = await RoleManager.FindByNameAsync(role);
                if (roleData == null)
                {
                    transactionScope.Dispose();
                    return new OperationResult() { Success = false, Message = $"Role {role} does not exist" };
                }
                var roleOpt = await UserManager.AddToRoleAsync(user, role);
                if (!roleOpt.Succeeded)
                {
                    transactionScope.Dispose();
                    return new OperationResult() { Success = false, Message = roleOpt.ToString() };
                }
                transactionScope.Complete();
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<Profile>> GetProfile(IdentityUser<Guid> user)
        {
            if (user is User)
            {
                var restUser = (User)user;
                try
                {

                    using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                    var personOperation = await _pbo.ReadAsync(restUser.ProfileId);
                    transactionScope.Complete();
                    return personOperation;
                }
                catch (Exception e)
                {
                    return new OperationResult<Profile>() { Success = false, Exception = e };
                }
            }
            return new OperationResult<Profile>() { Success = false, Message = "The user is not a RestaurantUser" };
        }

        public async Task<OperationResult<bool>> IsVisitor(Profile person)
        {
            var users = await UserManager.GetUsersInRoleAsync("Client");
            var user = users.FirstOrDefault(x => x.ProfileId == person.Id);
            if (user == null) return new OperationResult<bool>() { Success = true, Result = false, Message = "User is not a client" };
            else return new OperationResult<bool>() { Success = true, Result = false, Message = "User is a client" };
        }

        public async Task<OperationResult<bool>> IsCompany(Profile person)
        {
            var users = await UserManager.GetUsersInRoleAsync("Staff");
            var user = users.FirstOrDefault(x => x.ProfileId == person.Id);
            if (user == null) return new OperationResult<bool>() { Success = true, Result = false, Message = "User is not a staff member" };
            else return new OperationResult<bool>() { Success = true, Result = false, Message = "User is a staff member" };
        }

    }
}
