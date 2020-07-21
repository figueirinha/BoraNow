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
    public class CategoryInterestPointDataAccessObject
    {
        private BoraNowContext _context;

        public CategoryInterestPointDataAccessObject()
        {
            _context = new BoraNowContext();
        }

        #region List
        public List<CategoryInterestPoint> List()
        {
            return _context.Set<CategoryInterestPoint>().ToList();
        }

        public async Task<List<CategoryInterestPoint>> ListAsync()
        {
            return await _context.Set<CategoryInterestPoint>().ToListAsync();
        }
        #endregion

        #region Create
        public void Create(CategoryInterestPoint category)
        {
            _context.Category.Add(category);
            _context.SaveChanges();
        }

        public async Task CreateAsync(CategoryInterestPoint category)
        {
            await _context.Category.AddAsync(category);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public CategoryInterestPoint Read(Guid id)
        {
            return _context.Category.FirstOrDefault(x => x.Id == id);
        }

        public async Task<CategoryInterestPoint> ReadAsync(Guid id)
        {
            //Func<Category> result = () => _context.Category.FirstOrDefault(x => x.Id == id);
            //return await new Task<Category>(result);
            return await Task.Run(() => _context.Set<CategoryInterestPoint>().FirstOrDefault(x => x.Id == id));


        }
        #endregion

        #region Update
        public void Update(CategoryInterestPoint category)
        {
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(CategoryInterestPoint category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(CategoryInterestPoint category)
        {
            category.IsDeleted = true;
            Update(category);
        }
        public void Delete(Guid id)
        {
            var item = Read(id);
            if (item == null) return;
            Delete(item);
        }
        public async Task DeleteAsync(CategoryInterestPoint category)
        {
            category.IsDeleted = true;
            await UpdateAsync(category);
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
