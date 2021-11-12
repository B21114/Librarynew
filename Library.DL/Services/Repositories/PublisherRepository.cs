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
            var sortmethod = Publisher.GetSortExpressions(sortPole);
            var query = _dataBaseContext.Publishers.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(t => t.Name.ToLower().Contains(filter) || t.City.ToLower().Contains(filter));
            }
            return query.OrderBy(sortmethod).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }


        public async Task<Publisher> Get(Guid id)
        {
            return await _dataBaseContext.Publishers.FindAsync(id);
        }

        public async Task<Publisher> Update(Guid id, Publisher publisher)
        {
            await _dataBaseContext.Publishers.FindAsync(id);
            _dataBaseContext.Entry(publisher).State = EntityState.Modified;
            await _dataBaseContext.SaveChangesAsync();
            return publisher;
        }
    }
}
