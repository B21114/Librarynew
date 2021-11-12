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
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DataBaseContext _dataBaseContext;

        public AuthorRepository(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public async Task<Author> Add(Author author)
        {
            await _dataBaseContext.Authors.AddAsync(author);
            await _dataBaseContext.SaveChangesAsync();
            return author;
        }

        public async Task<bool> Delete(Author author)
        {
            _dataBaseContext.Entry(author).State = EntityState.Deleted;
            await _dataBaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Author>> Get()
        {
            return await _dataBaseContext.Authors.ToListAsync();
        }

        public async Task<IEnumerable<Author>> Get(int page, int pageSize, string filter, string sortPole)
        {
            var sortmethod = Author.GetSortExpressions(sortPole);
            var query = _dataBaseContext.Authors.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(t => t.Firstname.ToLower().Contains(filter) || t.Firstname.Contains(filter));
            }
            return query.OrderBy(sortmethod).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<Author> Get(Guid id)
        {
            return await _dataBaseContext.Authors.FindAsync(id);
        }

        public async Task<Author> Update(Guid id, Author author)
        {
            await _dataBaseContext.Authors.FindAsync(id);
            _dataBaseContext.Entry(author).State = EntityState.Modified;
            await _dataBaseContext.SaveChangesAsync();
            return author;
        }
    }
}
