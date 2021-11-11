using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DL.Services.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> Get();
        Task<IEnumerable<Book>> Get(int page, int pageSize, string filter, string sortPole);
        Task<Book> Get(Guid id);
        Task<Book> Add(Book book);
        Task<Book> Update(Guid id, Book book);
        Task<bool> Delete(Book book);

    }
}
