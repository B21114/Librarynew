using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BL.Services.Interfaces
{
    public interface IPublisherProvider
    {
        Task<IEnumerable<Publisher>> Get();
        Task<Publisher> Add(Publisher publisher);
        Task<IEnumerable<Publisher>> Get(int page, int pageSize, string pole, string sort);
        Task<Publisher> Get(Guid id);
        Task<Publisher> Update(Guid id, Publisher publisher);
        Task<bool> Delete(Guid id);
    }
}
