using Microsoft.EntityFrameworkCore;
using Recodme.RD.BoraNow.DataAccessLayer.Context;
using Recodme.RD.BoraNow.DataLayer.Meteo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.DataAccessLayer.DataAccessObjects.Meteo
{
    public class MeteorologyDataAccessObject
    {
        private BoraNowContext _context;

        public MeteorologyDataAccessObject()
        {
            _context = new BoraNowContext();
        }

        #region List
        public List<Meteorology> List()
        {
            return _context.Set<Meteorology>().ToList();
        }

        public async Task<List<Meteorology>> ListAsync()
        {
            return await _context.Set<Meteorology>().ToListAsync();
        }
        #endregion

        #region Create
        public void Create(Meteorology meteorology)
        {
            _context.Meteorology.Add(meteorology);
            _context.SaveChanges();
        }

        public async Task CreateAsync(Meteorology meteorology)
        {
            await _context.Meteorology.AddAsync(meteorology);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public Meteorology Read(Guid id)
        {
            return _context.Meteorology.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Meteorology> ReadAsync(Guid id)
        {
            //Func<Category> result = () => _context.Category.FirstOrDefault(x => x.Id == id);
            //return await new Task<Category>(result);
            return await Task.Run(() => _context.Set<Meteorology>().FirstOrDefault(x => x.Id == id));
        }
        #endregion

        #region Update
        public void Update(Meteorology meteorology)
        {
            _context.Entry(meteorology).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(Meteorology meteorology)
        {
            _context.Entry(meteorology).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(Meteorology meteorology)
        {
            meteorology.IsDeleted = true;
            Update(meteorology);
        }
        public void Delete(Guid id)
        {
            var item = Read(id);
            if (item == null) return;
            Delete(item);
        }
        public async Task DeleteAsync(Meteorology meteorology)
        {
            meteorology.IsDeleted = true;
            await UpdateAsync(meteorology);
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
