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
    public class BookRepository : TEntityRepository<Book>, IBookRepository
    {
        private readonly DataBaseContext _dataBaseContext;

        public BookRepository(DataBaseContext dataBaseContext) : base(dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public override async Task<Book> Add(Book book)
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

        //public async Task<bool> Delete(Book book)
        //{
        //    _dataBaseContext.Entry(book).State = EntityState.Deleted;
        //    await _dataBaseContext.SaveChangesAsync();
        //    return true;
        //}

        public override async Task<IEnumerable<Book>> Get()
        {
            return await _dataBaseContext.Books.Include(p => p.Authors).Include(p => p.Publisher).ToListAsync();
        }

        public override async Task<IEnumerable<Book>> Get(int page, int pageSize, string filter, string sortPole)
        {
            var sortmethod = Book.GetSortExpressions(sortPole);
            var query = _dataBaseContext.Books.Include(p => p.Authors).Include(p => p.Publisher).AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(t => t.Name.Contains(filter)
                || t.Publisher.City.Contains(filter)
                || t.Authors.Any(a => a.Lastname.Contains(filter))
                || t.Authors.Any(a => a.Firstname.Contains(filter))
                || t.Authors.Any(a => a.Patronymic.Contains(filter)));
            }
            return query.OrderBy(sortmethod).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public override async Task<Book> Get(Guid id)
        {
            return await _dataBaseContext.Books.Include(p => p.Authors).Include(p => p.Publisher)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        //public async Task<Book> Update(Guid id, Book book)
        //{
        //    await _dataBaseContext.Books.FindAsync(id);
        //    _dataBaseContext.Entry(book).State = EntityState.Modified;
        //    await _dataBaseContext.SaveChangesAsync();
        //    return book;
        //}
    }
}
