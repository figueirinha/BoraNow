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
    public class CategoryQuizAnswerDataAccessObject
    {

        private BoraNowContext _context;

        public CategoryQuizAnswerDataAccessObject()
        {
            _context = new BoraNowContext();
        }

        #region List
        public List<CategoryQuizAnswer> List()
        {
            return _context.Set<CategoryQuizAnswer>().ToList();
        }

        public async Task<List<CategoryQuizAnswer>> ListAsync()
        {
            return await _context.Set<CategoryQuizAnswer>().ToListAsync();
        }
        #endregion

        #region Create
        public void Create(CategoryQuizAnswer categoryQuizAnswer)
        {
            _context.CategoryQuizAnswer.Add(categoryQuizAnswer);
            _context.SaveChanges();
        }

        public async Task CreateAsync(CategoryQuizAnswer categoryQuizAnswer)
        {
            await _context.CategoryQuizAnswer.AddAsync(categoryQuizAnswer);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public CategoryQuizAnswer Read(Guid id)
        {
            return _context.CategoryQuizAnswer.FirstOrDefault(x => x.Id == id);
        }

        public async Task<CategoryQuizAnswer> ReadAsync(Guid id)
        {
            Func<CategoryQuizAnswer> result = () => _context.CategoryQuizAnswer.FirstOrDefault(x => x.Id == id);
            return await new Task<CategoryQuizAnswer>(result);


        }
        #endregion

        #region Update
        public void Update(CategoryQuizAnswer categoryQuizAnswer)
        {
            _context.Entry(categoryQuizAnswer).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(CategoryQuizAnswer categoryQuizAnswer)
        {
            _context.Entry(categoryQuizAnswer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(CategoryQuizAnswer categoryQuizAnswer)
        {
            categoryQuizAnswer.IsDeleted = true;
            Update(categoryQuizAnswer);
        }
        public void Delete(Guid id)
        {
            var item = Read(id);
            if (item == null) return;
            Delete(item);
        }
        public async Task DeleteAsync(CategoryQuizAnswer categoryQuizAnswer)
        {
            categoryQuizAnswer.IsDeleted = true;
            await UpdateAsync(categoryQuizAnswer);
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
