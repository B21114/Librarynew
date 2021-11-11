using Library.DL.Services.Interfaces;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DL.Services.Repositories
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly DataBaseContext _dataBaseContext;

        public PublisherRepository(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public async Task<Publisher> Add(Publisher publisher)
        {
            await _dataBaseContext.Publishers.AddAsync(publisher);
            await _dataBaseContext.SaveChangesAsync();
            return publisher;
        }

        public async Task<bool> Delete(Publisher publisher)
        {
            _dataBaseContext.Entry(publisher).State = EntityState.Deleted;
            await _dataBaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Publisher>> Get()
        {
            return await _dataBaseContext.Publishers.ToListAsync();
        }

        public async Task<IEnumerable<Publisher>> Get(int page, int pageSize, string filter, string sortPole)
        {
            var method = Publisher.GetSortExpressions(sortPole);
            if (filter == string.Empty || filter == null)
                return await _dataBaseContext.Publishers
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .OrderBy(method)
               .ToListAsync();
            else
                return await _dataBaseContext.Publishers
                   .Where(t => t.Name.ToLower().Contains(filter) || t.City.ToLower().Contains(filter))
                   .Skip((page - 1) * pageSize)
                   .Take(pageSize)
                   .OrderBy(method)
                   .ToListAsync();
        }
        public async Task<Publisher> Get(Guid id)
        {
            return await _dataBaseContext.Publishers.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Publisher> Update(Guid id, Publisher publisher)
        {
            await _dataBaseContext.Publishers.FirstOrDefaultAsync(u => u.Id == id);
            _dataBaseContext.Entry(publisher).State = EntityState.Modified;
            await _dataBaseContext.SaveChangesAsync();
            return publisher;
        }
    }
}
