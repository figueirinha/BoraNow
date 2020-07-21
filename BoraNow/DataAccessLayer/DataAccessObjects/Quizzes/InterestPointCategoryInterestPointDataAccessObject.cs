using Microsoft.EntityFrameworkCore;
using Recodme.RD.BoraNow.DataAccessLayer.Context;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.DataAccessLayer.DataAccessObjects.Quizzes
{
    public class InterestPointCategoryInterestPointDataAccessObject
    {
        private BoraNowContext _context;

        public InterestPointCategoryInterestPointDataAccessObject()
        {
            _context = new BoraNowContext();
        }

        #region List
        public List<InterestPointCategoryInterestPoint> List()
        {
            return _context.Set<InterestPointCategoryInterestPoint>().ToList();
        }

        public async Task<List<InterestPointCategoryInterestPoint>> ListAsync()
        {
            return await _context.Set<InterestPointCategoryInterestPoint>().ToListAsync();
        }
        #endregion

        #region Create
        public void Create(InterestPointCategoryInterestPoint interestPointCategory)
        {
            _context.InterestPointCategory.Add(interestPointCategory);
            _context.SaveChanges();
        }

        public async Task CreateAsync(InterestPointCategoryInterestPoint interestPointCategory)
        {
            await _context.InterestPointCategory.AddAsync(interestPointCategory);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public InterestPointCategoryInterestPoint Read(Guid id)
        {
            return _context.InterestPointCategory.FirstOrDefault(x => x.Id == id);
        }

        public async Task<InterestPointCategoryInterestPoint> ReadAsync(Guid id)
        {
            //Func<InterestPointCategory> result = () => _context.InterestPointCategory.FirstOrDefault(x => x.Id == id);
            //return await new Task<InterestPointCategory>(result);
            return await Task.Run(() => _context.Set<InterestPointCategoryInterestPoint>().FirstOrDefault(x => x.Id == id));


        }
        #endregion

        #region Update
        public void Update(InterestPointCategoryInterestPoint interestPointCategory)
        {
            _context.Entry(interestPointCategory).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(InterestPointCategoryInterestPoint interestPointCategory)
        {
            _context.Entry(interestPointCategory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(InterestPointCategoryInterestPoint interestPointCategory)
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
        public async Task DeleteAsync(InterestPointCategoryInterestPoint interestPointCategory)
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
