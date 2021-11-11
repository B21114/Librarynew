using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DL.Services.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> Get();
        Task<IEnumerable<Author>> Get(int page, int pageSize, string filter, string sortPole);
        Task<Author> Get(Guid id);
        Task<Author> Add(Author author);
        Task<Author> Update(Guid id, Author author);
        Task<bool> Delete(Author author);

    }
}
