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
    public class VisitorDataAccessObject
    {
        private BoraNowContext _context;

        public VisitorDataAccessObject()
        {
            _context = new BoraNowContext();
        }

        #region List
        public List<Visitor> List()
        {
            return _context.Set<Visitor>().ToList();
        }

        public async Task<List<Visitor>> ListAsync()
        {
            return await _context.Set<Visitor>().ToListAsync();
        }
        #endregion

        #region Create
        public void Create(Visitor visitor)
        {
            _context.Visitor.Add(visitor);
            _context.SaveChanges();
        }

        public async Task CreateAsync(Visitor visitor)
        {
            await _context.Visitor.AddAsync(visitor);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public Visitor Read(Guid id)
        {
            return _context.Visitor.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Visitor> ReadAsync(Guid id)
        {
            //Func<Visitor> result = () => _context.Visitor.FirstOrDefault(x => x.Id == id);
            //return await new Task<Visitor>(result);
            return await Task.Run(() => _context.Set<Visitor>().FirstOrDefault(x => x.Id == id));


        }
        #endregion

        #region Update
        public void Update(Visitor visitor)
        {
            _context.Entry(visitor).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(Visitor visitor)
        {
            _context.Entry(visitor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(Visitor visitor)
        {
            visitor.IsDeleted = true;
            Update(visitor);
        }
        public void Delete(Guid id)
        {
            var item = Read(id);
            if (item == null) return;
            Delete(item);
        }
        public async Task DeleteAsync(Visitor visitor)
        {
            visitor.IsDeleted = true;
            await UpdateAsync(visitor);
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
