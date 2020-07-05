using Microsoft.EntityFrameworkCore;
using Recodme.RD.BoraNow.DataAccessLayer.Context;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.DataAccessLayer.DataAccessObjects.Quizzes
{
    public class QuizAnswerDataAccessObject
    {
        private BoraNowContext _context;

        public QuizAnswerDataAccessObject()
        {
            _context = new BoraNowContext();
        }

        #region List
        public List<QuizAnswer> List()
        {
            return _context.Set<QuizAnswer>().ToList();
        }

        public async Task<List<QuizAnswer>> ListAsync()
        {
            return await _context.Set<QuizAnswer>().ToListAsync();
        }
        #endregion

        #region Create
        public void Create(QuizAnswer quizAnswer)
        {
            _context.QuizAnswer.Add(quizAnswer);
            _context.SaveChanges();
        }

        public async Task CreateAsync(QuizAnswer quizAnswer)
        {
            await _context.QuizAnswer.AddAsync(quizAnswer);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public QuizAnswer Read(Guid id)
        {
            return _context.QuizAnswer.FirstOrDefault(x => x.Id == id);
        }

        public async Task<QuizAnswer> ReadAsync(Guid id)
        {
            Func<QuizAnswer> result = () => _context.QuizAnswer.FirstOrDefault(x => x.Id == id);
            return await new Task<QuizAnswer>(result);


        }
        #endregion

        #region Update
        public void Update(QuizAnswer quizAnswer)
        {
            _context.Entry(quizAnswer).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(QuizAnswer quizAnswer)
        {
            _context.Entry(quizAnswer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(QuizAnswer quizAnswer)
        {
            quizAnswer.IsDeleted = true;
            Update(quizAnswer);
        }
        public void Delete(Guid id)
        {
            var item = Read(id);
            if (item == null) return;
            Delete(item);
        }
        public async Task DeleteAsync(QuizAnswer quizAnswer)
        {
            quizAnswer.IsDeleted = true;
            await UpdateAsync(quizAnswer);
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
}
