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
    public class ProfileDataAccessObject
    {
        private BoraNowContext _context;

        public ProfileDataAccessObject()
        {
            _context = new BoraNowContext();
        }

        #region List
        public List<Profile> List()
        {
            return _context.Set<Profile>().ToList();
        }

        public async Task<List<Profile>> ListAsync()
        {
            return await _context.Set<Profile>().ToListAsync();
        }
        #endregion

        #region Create
        public void Create(Profile profile)
        {
            _context.Profile.Add(profile);
            _context.SaveChanges();
        }

        public async Task CreateAsync(Profile profile)
        {
            await _context.Profile.AddAsync(profile);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public Profile Read(Guid id)
        {
            return _context.Profile.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Profile> ReadAsync(Guid id)
        {
            //Func<Category> result = () => _context.Category.FirstOrDefault(x => x.Id == id);
            //return await new Task<Category>(result);
            return await Task.Run(() => _context.Set<Profile>().FirstOrDefault(x => x.Id == id));
        }
        #endregion

        #region Update
        public void Update(Profile profile)
        {
            _context.Entry(profile).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(Profile profile)
        {
            _context.Entry(profile).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(Profile profile)
        {
            profile.IsDeleted = true;
            Update(profile);
        }
        public void Delete(Guid id)
        {
            var item = Read(id);
            if (item == null) return;
            Delete(item);
        }
        public async Task DeleteAsync(Profile profile)
        {
            profile.IsDeleted = true;
            await UpdateAsync(profile);
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
