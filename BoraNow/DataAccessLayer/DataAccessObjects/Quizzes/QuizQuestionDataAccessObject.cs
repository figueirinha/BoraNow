using Microsoft.EntityFrameworkCore;
using Recodme.RD.BoraNow.DataAccessLayer.Context;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.DataAccessLayer.DataAccessObjects.Quizzes
{
    public class QuizQuestionDataAccessObject
    {
        private BoraNowContext _context;

        public QuizQuestionDataAccessObject()
        {
            _context = new BoraNowContext();
        }

        #region List
        public List<QuizQuestion> List()
        {
            return _context.Set<QuizQuestion>().ToList();
        }

        public async Task<List<QuizQuestion>> ListAsync()
        {
            return await _context.Set<QuizQuestion>().ToListAsync();
        }
        #endregion

        #region Create
        public void Create(QuizQuestion quizQuestion)
        {
            _context.QuizQuestion.Add(quizQuestion);
            _context.SaveChanges();
        }

        public async Task CreateAsync(QuizQuestion quizQuestion)
        {
            await _context.QuizQuestion.AddAsync(quizQuestion);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public QuizQuestion Read(Guid id)
        {
            return _context.QuizQuestion.FirstOrDefault(x => x.Id == id);
        }

        public async Task<QuizQuestion> ReadAsync(Guid id)
        {
            //Func<QuizQuestion> result = () => _context.QuizQuestion.FirstOrDefault(x => x.Id == id);
            //return await new Task<QuizQuestion>(result);
            return await Task.Run(() => _context.Set<QuizQuestion>().FirstOrDefault(x => x.Id == id));


        }
        #endregion

        #region Update
        public void Update(QuizQuestion quizQuestion)
        {
            _context.Entry(quizQuestion).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(QuizQuestion quizQuestion)
        {
            _context.Entry(quizQuestion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(QuizQuestion quizQuestion)
        {
            quizQuestion.IsDeleted = true;
            Update(quizQuestion);
        }
        public void Delete(Guid id)
        {
            var item = Read(id);
            if (item == null) return;
            Delete(item);
        }
        public async Task DeleteAsync(QuizQuestion quizQuestion)
        {
            quizQuestion.IsDeleted = true;
            await UpdateAsync(quizQuestion);
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
