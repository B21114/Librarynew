using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BL.Services.Interfaces
{
    public interface IBookProvider
    {
        Task<IEnumerable<Book>> Get();
        Task<Book> Add(Book book);
        Task<IEnumerable<Book>> Get(int page, int pageSize, string filter, string sortPole);
        Task<Book> Get(Guid id);
        Task<Book> Update(Guid id, Book book);
        Task<bool> Delete(Guid id);
    }
}
