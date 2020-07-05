using Microsoft.EntityFrameworkCore;
using Recodme.RD.BoraNow.DataAccessLayer.Context;
using Recodme.RD.BoraNow.DataLayer.Quiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.DataAccessLayer.DataAccessObjects.Quiz
{
    public class InterestPointDataAccessObject
    {

        private BoraNowContext _context;

        public InterestPointDataAccessObject()
        {
            _context = new BoraNowContext();
        }

        #region List
        public List<InterestPoint> List()
        {
            return _context.Set<InterestPoint>().ToList();
        }

        public async Task<List<InterestPoint>> ListAsync()
        {
            return await _context.Set<InterestPoint>().ToListAsync();
        }
        #endregion

        #region Create
        public void Create(InterestPoint interestPoint)
        {
            _context.InterestPoint.Add(interestPoint);
            _context.SaveChanges();
        }

        public async Task CreateAsync(InterestPoint interestPoint)
        {
            await _context.InterestPoint.AddAsync(interestPoint);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public InterestPoint Read(Guid id)
        {
            return _context.InterestPoint.FirstOrDefault(x => x.Id == id);
        }

        public async Task<InterestPoint> ReadAsync(Guid id)
        {
            Func<InterestPoint> result = () => _context.InterestPoint.FirstOrDefault(x => x.Id == id);
            return await new Task<InterestPoint>(result);


        }
        #endregion

        #region Update
        public void Update(InterestPoint interestPoint)
        {
            _context.Entry(interestPoint).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(InterestPoint interestPoint)
        {
            _context.Entry(interestPoint).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(InterestPoint interestPoint)
        {
            interestPoint.IsDeleted = true;
            Update(interestPoint);
        }
        public void Delete(Guid id)
        {
            var item = Read(id);
            if (item == null) return;
            Delete(item);
        }
        public async Task DeleteAsync(InterestPoint interestPoint)
        {
            interestPoint.IsDeleted = true;
            await UpdateAsync(interestPoint);
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
