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
    public class CategoryDataAccessObject
    {
        private BoraNowContext _context;

        public CategoryDataAccessObject()
        {
            _context = new BoraNowContext();
        }

        #region List
        public List<Category> List()
        {
            return _context.Set<Category>().ToList();
        }

        public async Task<List<Category>> ListAsync()
        {
            return await _context.Set<Category>().ToListAsync();
        }
        #endregion

        #region Create
        public void Create(Category category)
        {
            _context.Category.Add(category);
            _context.SaveChanges();
        }

        public async Task CreateAsync(Category category)
        {
            await _context.Category.AddAsync(category);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public Category Read(Guid id)
        {
            return _context.Category.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Category> ReadAsync(Guid id)
        {
            Func<Category> result = () => _context.Category.FirstOrDefault(x => x.Id == id);
            return await new Task<Category>(result);


        }
        #endregion

        #region Update
        public void Update(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(Category category)
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
        public async Task DeleteAsync(Category category)
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
