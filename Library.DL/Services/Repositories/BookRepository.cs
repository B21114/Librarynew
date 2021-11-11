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
    public class BookRepository : IBookRepository
    {
        private readonly DataBaseContext _dataBaseContext;

        public BookRepository(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public async Task<Book> Add(Book book)
        {
            foreach (var item in book.Authors)
            {
                _dataBaseContext.Entry(item).State = EntityState.Unchanged;
            }
            _dataBaseContext.Entry(book.Publisher).State = EntityState.Unchanged;
            await _dataBaseContext.Books.AddAsync(book);
            await _dataBaseContext.SaveChangesAsync();
            return book;
        }

        public async Task<bool> Delete(Book book)
        {
            _dataBaseContext.Entry(book).State = EntityState.Deleted;
            await _dataBaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Book>> Get()
        {
            return await _dataBaseContext.Books.Include(p => p.Authors).Include(p => p.Publisher).ToListAsync();
        }

        public async Task<IEnumerable<Book>> Get(int page, int pageSize, string filter, string sortPole)
        {
            var method = Book.GetSortExpressions(sortPole);
            if (filter == string.Empty || filter == null)
                return await _dataBaseContext.Books
               .Include(p => p.Authors).Include(p => p.Publisher)
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .OrderBy(method)
               .ToListAsync();
            else
                return await _dataBaseContext.Books
               .Include(p => p.Authors).Include(p => p.Publisher)
               .Where(t => t.Name.ToLower().Contains(filter) || t.Publisher.City.Contains(filter)
               || t.Publisher.Name.Contains(filter) || t.Authors.Any(y => y.Lastname.Contains(filter))
               || t.Authors.Any(y => y.Firstname.Contains(filter)) || t.Authors.Any(y => y.Patronymic.Contains(filter))
               || t.Authors.Any(y => y.Activity.Contains(filter)))
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .OrderBy(method)
               .ToListAsync();
        }

        public async Task<Book> Get(Guid id)
        {
            return await _dataBaseContext.Books.Include(p => p.Authors).Include(p => p.Publisher)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Book> Update(Guid id, Book book)
        {
            await _dataBaseContext.Books.FirstOrDefaultAsync(u => u.Id == id);
            _dataBaseContext.Entry(book).State = EntityState.Modified;
            await _dataBaseContext.SaveChangesAsync();
            return book;
        }
    }
}
