using Microsoft.EntityFrameworkCore;
using Recodme.RD.BoraNow.DataAccessLayer.Context;
using Recodme.RD.BoraNow.DataLayer.Feedbacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.DataAccessLayer.DataAccessObjects.Feedbacks
{
    public class FeedbackDataAccessObject
    {
        private BoraNowContext _context;

        public FeedbackDataAccessObject()
        {
            _context = new BoraNowContext();
        }

        #region List
        public List<Feedback> List()
        {
            return _context.Set<Feedback>().ToList();
        }

        public async Task<List<Feedback>> ListAsync()
        {
            return await _context.Set<Feedback>().ToListAsync();
        }
        #endregion

        #region Create
        public void Create(Feedback feedback)
        {
            _context.Feedback.Add(feedback);
            _context.SaveChanges();
        }

        public async Task CreateAsync(Feedback feedback)
        {
            await _context.Feedback.AddAsync(feedback);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public Feedback Read(Guid id)
        {
            return _context.Feedback.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Feedback> ReadAsync(Guid id)
        {
            //Func<Category> result = () => _context.Category.FirstOrDefault(x => x.Id == id);
            //return await new Task<Category>(result);
            return await Task.Run(() => _context.Set<Feedback>().FirstOrDefault(x => x.Id == id));
        }
        #endregion

        #region Update
        public void Update(Feedback feedback)
        {
            _context.Entry(feedback).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(Feedback feedback)
        {
            _context.Entry(feedback).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(Feedback feedback)
        {
            feedback.IsDeleted = true;
            Update(feedback);
        }
        public void Delete(Guid id)
        {
            var item = Read(id);
            if (item == null) return;
            Delete(item);
        }
        public async Task DeleteAsync(Feedback feedback)
        {
            feedback.IsDeleted = true;
            await UpdateAsync(feedback);
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
