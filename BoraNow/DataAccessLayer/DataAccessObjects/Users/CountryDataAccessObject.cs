using Microsoft.EntityFrameworkCore;
using Recodme.RD.BoraNow.DataAccessLayer.Context;
using Recodme.RD.BoraNow.DataLayer.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.DataAccessLayer.DataAccessObjects.Users
{
    public class CountryDataAccessObject
    {
        private BoraNowContext _context;

        public CountryDataAccessObject()
        {
            _context = new BoraNowContext();
        }

        #region List
        public List<Country> List()
        {
            return _context.Set<Country>().ToList();
        }

        public async Task<List<Country>> ListAsync()
        {
            return await _context.Set<Country>().ToListAsync();
        }
        #endregion

        #region Create
        public void Create(Country country)
        {
            _context.Country.Add(country);
            _context.SaveChanges();
        }

        public async Task CreateAsync(Country country)
        {
            await _context.Country.AddAsync(country);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public Country Read(Guid id)
        {
            return _context.Country.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Country> ReadAsync(Guid id)
        {
            //Func<Category> result = () => _context.Category.FirstOrDefault(x => x.Id == id);
            //return await new Task<Category>(result);
            return await Task.Run(() => _context.Set<Country>().FirstOrDefault(x => x.Id == id));
        }
        #endregion

        #region Update
        public void Update(Country country)
        {
            _context.Entry(country).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(Country country)
        {
            _context.Entry(country).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(Country country)
        {
            country.IsDeleted = true;
            Update(country);
        }
        public void Delete(Guid id)
        {
            var item = Read(id);
            if (item == null) return;
            Delete(item);
        }
        public async Task DeleteAsync(Country country)
        {
            country.IsDeleted = true;
            await UpdateAsync(country);
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
