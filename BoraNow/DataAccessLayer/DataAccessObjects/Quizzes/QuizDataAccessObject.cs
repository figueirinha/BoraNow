using Microsoft.EntityFrameworkCore;
using Recodme.RD.BoraNow.DataAccessLayer.Context;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.DataAccessLayer.DataAccessObjects.Quizzes
{
    public class QuizDataAccessObject
    {
        private BoraNowContext _context;

        public QuizDataAccessObject()
        {
            _context = new BoraNowContext();
        }

        #region List
        public List<Quiz> List()
        {
            return _context.Set<Quiz>().ToList();
        }

        public async Task<List<Quiz>> ListAsync()
        {
            return await _context.Set<Quiz>().ToListAsync();
        }
        #endregion

        #region Create
        public void Create(Quiz quiz)
        {
            _context.Quiz.Add(quiz);
            _context.SaveChanges();
        }

        public async Task CreateAsync(Quiz Quiz)
        {
            await _context.Quiz.AddAsync(Quiz);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public Quiz Read(Guid id)
        {
            return _context.Quiz.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Quiz> ReadAsync(Guid id)
        {
            //Func<Quiz> result = () => _context.Quiz.FirstOrDefault(x => x.Id == id);
            //return await new Task<Quiz>(result);
            return await Task.Run(() => _context.Set<Quiz>().FirstOrDefault(x => x.Id == id));


        }
        #endregion

        #region Update
        public void Update(Quiz Quiz)
        {
            _context.Entry(Quiz).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(Quiz Quiz)
        {
            _context.Entry(Quiz).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(Quiz Quiz)
        {
            Quiz.IsDeleted = true;
            Update(Quiz);
        }
        public void Delete(Guid id)
        {
            var item = Read(id);
            if (item == null) return;
            Delete(item);
        }
        public async Task DeleteAsync(Quiz Quiz)
        {
            Quiz.IsDeleted = true;
            await UpdateAsync(Quiz);
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
