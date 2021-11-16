using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DL.Services.Interfaces
{
    public interface IPublisherRepository : ITEntityRepository<Publisher>
    {
        Task<IEnumerable<Publisher>> Get();
        Task<IEnumerable<Publisher>> Get(int page, int pageSize, string pole, string sort);
        Task<Publisher> Get(Guid id);
        Task<Publisher> Add(Publisher publisher);
        Task<Publisher> Update(Guid id, Publisher publisher);
        Task<bool> Delete(Publisher publisher);

    }
}
