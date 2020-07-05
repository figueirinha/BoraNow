using Microsoft.EntityFrameworkCore;
using Recodme.RD.BoraNow.DataAccessLayer.Context;
using Recodme.RD.BoraNow.DataLayer.Quiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.DataAccessLayer.DataAccessObjects.Quizzes
{
    public class InterestPointCategoryDataAccessObject
    {
        private BoraNowContext _context;

        public InterestPointCategoryDataAccessObject()
        {
            _context = new BoraNowContext();
        }

        #region List
        public List<InterestPointCategory> List()
        {
            return _context.Set<InterestPointCategory>().ToList();
        }

        public async Task<List<InterestPointCategory>> ListAsync()
        {
            return await _context.Set<InterestPointCategory>().ToListAsync();
        }
        #endregion

        #region Create
        public void Create(InterestPointCategory interestPointCategory)
        {
            _context.InterestPointCategory.Add(interestPointCategory);
            _context.SaveChanges();
        }

        public async Task CreateAsync(InterestPointCategory interestPointCategory)
        {
            await _context.InterestPointCategory.AddAsync(interestPointCategory);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public InterestPointCategory Read(Guid id)
        {
            return _context.InterestPointCategory.FirstOrDefault(x => x.Id == id);
        }

        public async Task<InterestPointCategory> ReadAsync(Guid id)
        {
            Func<InterestPointCategory> result = () => _context.InterestPointCategory.FirstOrDefault(x => x.Id == id);
            return await new Task<InterestPointCategory>(result);


        }
        #endregion

        #region Update
        public void Update(InterestPointCategory interestPointCategory)
        {
            _context.Entry(interestPointCategory).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(InterestPointCategory interestPointCategory)
        {
            _context.Entry(interestPointCategory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(InterestPointCategory interestPointCategory)
        {
            interestPointCategory.IsDeleted = true;
            Update(interestPointCategory);
        }
        public void Delete(Guid id)
        {
            var item = Read(id);
            if (item == null) return;
            Delete(item);
        }
        public async Task DeleteAsync(InterestPointCategory interestPointCategory)
        {
            interestPointCategory.IsDeleted = true;
            await UpdateAsync(interestPointCategory);
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
