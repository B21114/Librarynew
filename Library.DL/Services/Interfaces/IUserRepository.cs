using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DL.Services.Interfaces
{
    public interface IUserRepository : ITEntityRepository<User>
    {
        Task<IEnumerable<User>> Get();
        Task<IEnumerable<User>> Get(int page, int pageSize, string filter, string sortPole);
        Task<User> Get(Guid id);
        Task<User> Get(string email);
        //Task<User> Add(User user);
        Task<User> Update(Guid id, User user);
        Task<bool> Delete(User user);

    }
}
