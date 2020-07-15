using Microsoft.EntityFrameworkCore;
using Recodme.RD.BoraNow.DataAccessLayer.Context;
using Recodme.RD.BoraNow.DataLayer.Newsletters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.DataAccessLayer.DataAccessObjects.Newsletters
{
    public class NewsletterDataAccessObject
    {
        private BoraNowContext _context;

        public NewsletterDataAccessObject()
        {
            _context = new BoraNowContext();
        }

        #region List
        public List<Newsletter> List()
        {
            return _context.Set<Newsletter>().ToList();
        }

        public async Task<List<Newsletter>> ListAsync()
        {
            return await _context.Set<Newsletter>().ToListAsync();
        }
        #endregion

        #region Create
        public void Create(Newsletter newsletter)
        {
            _context.Newsletter.Add(newsletter);
            _context.SaveChanges();
        }

        public async Task CreateAsync(Newsletter newsletter)
        {
            await _context.Newsletter.AddAsync(newsletter);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public Newsletter Read(Guid id)
        {
            return _context.Newsletter.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Newsletter> ReadAsync(Guid id)
        {
            //Func<Category> result = () => _context.Category.FirstOrDefault(x => x.Id == id);
            //return await new Task<Category>(result);
            return await Task.Run(() => _context.Set<Newsletter>().FirstOrDefault(x => x.Id == id));
        }
        #endregion

        #region Update
        public void Update(Newsletter newsletter)
        {
            _context.Entry(newsletter).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(Newsletter newsletter)
        {
            _context.Entry(newsletter).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(Newsletter newsletter)
        {
            newsletter.IsDeleted = true;
            Update(newsletter);
        }
        public void Delete(Guid id)
        {
            var item = Read(id);
            if (item == null) return;
            Delete(item);
        }
        public async Task DeleteAsync(Newsletter newsletter)
        {
            newsletter.IsDeleted = true;
            await UpdateAsync(newsletter);
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
