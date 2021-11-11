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
            var method = Author.GetSortExpressions(sortPole);
            if (filter == string.Empty || filter == null)
                return await _dataBaseContext.Authors
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .OrderBy(method)
               .ToListAsync();
            else
                return await _dataBaseContext.Authors
                   .Where(t => t.Firstname.ToLower().Contains(filter) || t.Firstname.Contains(filter))
                   .Skip((page - 1) * pageSize)
                   .Take(pageSize)
                   .OrderBy(method)
                   .ToListAsync();
        }

        public async Task<Author> Get(Guid id)
        {
            return await _dataBaseContext.Authors.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Author> Update(Guid id, Author author)
        {
            await _dataBaseContext.Authors.FirstOrDefaultAsync(u => u.Id == id);
            _dataBaseContext.Entry(author).State = EntityState.Modified;
            await _dataBaseContext.SaveChangesAsync();
            return author;
        }
    }
}
