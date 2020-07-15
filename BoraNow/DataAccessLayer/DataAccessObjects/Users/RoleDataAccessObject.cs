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
    public class RoleDataAccessObject
    {
        private BoraNowContext _context;

        public RoleDataAccessObject()
        {
            _context = new BoraNowContext();
        }

        #region List
        public List<Role> List()
        {
            return _context.Set<Role>().ToList();
        }

        public async Task<List<Role>> ListAsync()
        {
            return await _context.Set<Role>().ToListAsync();
        }
        #endregion

        #region Create
        public void Create(Role role)
        {
            _context.Role.Add(role);
            _context.SaveChanges();
        }

        public async Task CreateAsync(Role role)
        {
            await _context.Role.AddAsync(role);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public Role Read(Guid id)
        {
            return _context.Role.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Role> ReadAsync(Guid id)
        {
            //Func<Role> result = () => _context.Role.FirstOrDefault(x => x.Id == id);
            //return await new Task<Role>(result);
            return await Task.Run(() => _context.Set<Role>().FirstOrDefault(x => x.Id == id));


        }
        #endregion

        #region Update
        public void Update(Role role)
        {
            _context.Entry(role).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(Role role)
        {
            _context.Entry(role).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(Role role)
        {
            role.IsDeleted = true;
            Update(role);
        }
        public void Delete(Guid id)
        {
            var item = Read(id);
            if (item == null) return;
            Delete(item);
        }
        public async Task DeleteAsync(Role role)
        {
            role.IsDeleted = true;
            await UpdateAsync(role);
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
