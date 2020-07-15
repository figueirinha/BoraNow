using Microsoft.EntityFrameworkCore;
using Recodme.RD.BoraNow.DataAccessLayer.Context;
using Recodme.RD.BoraNow.DataLayer.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.DataAccessLayer.DataAccessObjects.Users
{
    public class UserDataAccessObject
    {
        private BoraNowContext _context;

        public UserDataAccessObject()
        {
            _context = new BoraNowContext();
        }

        #region List
        public List<User> List()
        {
            return _context.Set<User>().ToList();
        }

        public async Task<List<User>> ListAsync()
        {
            return await _context.Set<User>().ToListAsync();
        }
        #endregion

        #region Create
        public void Create(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }

        public async Task CreateAsync(User user)
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public User Read(Guid id)
        {
            return _context.User.FirstOrDefault(x => x.Id == id);
        }

        public async Task<User> ReadAsync(Guid id)
        {
            //Func<User> result = () => _context.User.FirstOrDefault(x => x.Id == id);
            //return await new Task<User>(result);
            return await Task.Run(() => _context.Set<User>().FirstOrDefault(x => x.Id == id));


        }
        #endregion

        #region Update
        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(User user)
        {
            user.IsDeleted = true;
            Update(user);
        }
        public void Delete(Guid id)
        {
            var item = Read(id);
            if (item == null) return;
            Delete(item);
        }
        public async Task DeleteAsync(User user)
        {
            user.IsDeleted = true;
            await UpdateAsync(user);
        }
        public async Task DeleteAsync(Guid id)
        {
            var item = ReadAsync(id).Result;
            if (item == null) return;
            await DeleteAsync(item);
        }
        #endregion 
    }
}
