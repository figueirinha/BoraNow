using Microsoft.EntityFrameworkCore;
using Recodme.RD.BoraNow.DataAccessLayer.Context;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.DataAccessLayer.DataAccessObjects.Quizzes
{
    public class ResultInterestPointDataAccessObject
    {
        private BoraNowContext _context;

        public ResultInterestPointDataAccessObject()
        {
            _context = new BoraNowContext();
        }

        #region List
        public List<ResultInterestPoint> List()
        {
            return _context.Set<ResultInterestPoint>().ToList();
        }

        public async Task<List<ResultInterestPoint>> ListAsync()
        {
            return await _context.Set<ResultInterestPoint>().ToListAsync();
        }
        #endregion

        #region Create
        public void Create(ResultInterestPoint resultInterestPoint)
        {
            _context.ResultInterestPoint.Add(resultInterestPoint);
            _context.SaveChanges();
        }

        public async Task CreateAsync(ResultInterestPoint resultInterestPoint)
        {
            await _context.ResultInterestPoint.AddAsync(resultInterestPoint);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public ResultInterestPoint Read(Guid id)
        {
            return _context.ResultInterestPoint.FirstOrDefault(x => x.Id == id);
        }

        public async Task<ResultInterestPoint> ReadAsync(Guid id)
        {
            //Func<ResultInterestPoint> result = () => _context.ResultInterestPoint.FirstOrDefault(x => x.Id == id);
            //return await new Task<ResultInterestPoint>(result);
            return await Task.Run(() => _context.Set<ResultInterestPoint>().FirstOrDefault(x => x.Id == id));

        }
        #endregion

        #region Update
        public void Update(ResultInterestPoint resultInterestPoint)
        {
            _context.Entry(resultInterestPoint).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(ResultInterestPoint resultInterestPoint)
        {
            _context.Entry(resultInterestPoint).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(ResultInterestPoint resultInterestPoint)
        {
            resultInterestPoint.IsDeleted = true;
            Update(resultInterestPoint);
        }
        public void Delete(Guid id)
        {
            var item = Read(id);
            if (item == null) return;
            Delete(item);
        }
        public async Task DeleteAsync(ResultInterestPoint resultInterestPoint)
        {
            resultInterestPoint.IsDeleted = true;
            await UpdateAsync(resultInterestPoint);
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
