using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BL.Services.Interfaces
{
    public interface IUserProvider : IAuthentication
    {
        Task<IEnumerable<User>> Get();
        Task<User> Add(User user);
        Task<IEnumerable<User>> Get(int page, int pageSize, string pole, string sort);
        Task<User> Get(Guid id);
        Task<User> Update(Guid id, User user);
        Task<bool> Delete(Guid id);
    }
}
