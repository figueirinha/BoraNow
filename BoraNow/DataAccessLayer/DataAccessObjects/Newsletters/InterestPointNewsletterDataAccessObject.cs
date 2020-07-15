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
    public class InterestPointNewsletterDataAccessObject
    {
        private BoraNowContext _context;

        public InterestPointNewsletterDataAccessObject()
        {
            _context = new BoraNowContext();
        }

        #region List
        public List<InterestPointNewsletter> List()
        {
            return _context.Set<InterestPointNewsletter>().ToList();
        }

        public async Task<List<InterestPointNewsletter>> ListAsync()
        {
            return await _context.Set<InterestPointNewsletter>().ToListAsync();
        }
        #endregion

        #region Create
        public void Create(InterestPointNewsletter interestPointNewsletter)
        {
            _context.InterestPointNewsletter.Add(interestPointNewsletter);
            _context.SaveChanges();
        }

        public async Task CreateAsync(InterestPointNewsletter interestPointNewsletter)
        {
            await _context.InterestPointNewsletter.AddAsync(interestPointNewsletter);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public InterestPointNewsletter Read(Guid id)
        {
            return _context.InterestPointNewsletter.FirstOrDefault(x => x.Id == id);
        }

        public async Task<InterestPointNewsletter> ReadAsync(Guid id)
        {
            //Func<Category> result = () => _context.Category.FirstOrDefault(x => x.Id == id);
            //return await new Task<Category>(result);
            return await Task.Run(() => _context.Set<InterestPointNewsletter>().FirstOrDefault(x => x.Id == id));
        }
        #endregion

        #region Update
        public void Update(InterestPointNewsletter interestPointNewsletter)
        {
            _context.Entry(interestPointNewsletter).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(InterestPointNewsletter InterestPointNewsletter)
        {
            _context.Entry(InterestPointNewsletter).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(InterestPointNewsletter interestPointNewsletter)
        {
            interestPointNewsletter.IsDeleted = true;
            Update(interestPointNewsletter);
        }
        public void Delete(Guid id)
        {
            var item = Read(id);
            if (item == null) return;
            Delete(item);
        }
        public async Task DeleteAsync(InterestPointNewsletter interestPointNewsletter)
        {
            interestPointNewsletter.IsDeleted = true;
            await UpdateAsync(interestPointNewsletter);
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
