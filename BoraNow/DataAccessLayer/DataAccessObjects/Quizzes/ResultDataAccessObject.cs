using Microsoft.EntityFrameworkCore;
using Recodme.RD.BoraNow.DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.DataAccessLayer.DataAccessObjects.Quiz
{
    public class ResultDataAccessObject
    {
        private BoraNowContext _context;

        public ResultDataAccessObject()
        {
            _context = new BoraNowContext();
        }

        #region List
        public List<Result> List()
        {
            return _context.Set<Result>().ToList();
        }

        public async Task<List<Result>> ListAsync()
        {
            return await _context.Set<Result>().ToListAsync();
        }
        #endregion

        #region Create
        public void Create(Result result)
        {
            _context.Result.Add(result);
            _context.SaveChanges();
        }

        public async Task CreateAsync(Result result)
        {
            await _context.Result.AddAsync(result);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public Result Read(Guid id)
        {
            return _context.Result.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Result> ReadAsync(Guid id)
        {
            Func<Result> result = () => _context.Result.FirstOrDefault(x => x.Id == id);
            return await new Task<Result>(result);


        }
        #endregion

        #region Update
        public void Update(Result result)
        {
            _context.Entry(result).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(Result result)
        {
            _context.Entry(result).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(Result result)
        {
            result.IsDeleted = true;
            Update(result);
        }
        public void Delete(Guid id)
        {
            var item = Read(id);
            if (item == null) return;
            Delete(item);
        }
        public async Task DeleteAsync(Result result)
        {
            result.IsDeleted = true;
            await UpdateAsync(result);
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
