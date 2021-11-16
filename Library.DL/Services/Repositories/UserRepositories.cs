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
    public class UserRepository : TEntityRepository<User>, IUserRepository
    {
        private readonly DataBaseContext _dataBaseContext;

        public UserRepository(DataBaseContext dataBaseContext) : base(dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        //public override Task<User> Add(User entity)
        //{
        //    return base.Add(entity);
        //}

        //public async Task<User> Add(User user)
        //{
        //    await _dataBaseContext.Users.AddAsync(user);
        //    await _dataBaseContext.SaveChangesAsync();
        //    return user;
        //}

        //public async Task<bool> Delete(User user)
        //{
        //    _dataBaseContext.Entry(user).State = EntityState.Deleted;
        //    await _dataBaseContext.SaveChangesAsync();
        //    return true;
        //}

        //public async Task<IEnumerable<User>> Get()
        //{
        //    return await _dataBaseContext.Users.ToListAsync();
        //}

        public override async Task<IEnumerable<User>> Get(int page, int pageSize, string filter, string sortPole)
        {
            var sortmethod = User.GetSortExpressions(sortPole);
            var query = _dataBaseContext.Users.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(t => t.Name.ToLower().Contains(filter) || t.Email.Contains(filter));
            }
            return query.OrderBy(sortmethod).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        //public async Task<User> Get(Guid id)
        //{
        //    return await _dataBaseContext.Users.FindAsync(id);
        //}

        public async Task<User> Get(string email)
        {
            return await _dataBaseContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        //public async Task<User> Update(Guid id, User user)
        //{
        //    await _dataBaseContext.Users.FindAsync(id);
        //    _dataBaseContext.Entry(user).State = EntityState.Modified;
        //    await _dataBaseContext.SaveChangesAsync();
        //    return user;
        //}
    }
}
