using Library.BL.Services.Interfaces;
using Library.DL.Services.Interfaces;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;

namespace Library.BL.Services.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly IUserRepository _userRepository;

        public UserProvider(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Add(User user)
        {
            return await _userRepository.Add(user);
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var user = await _userRepository.Get(id);
                await _userRepository.Delete(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<User>> Get()
        {
            return await _userRepository.Get();
        }

        public async Task<IEnumerable<User>> Get(int page, int pageSize, string filter, string sort)
        {
            if (page < 1 || pageSize < 1)
            {
                return null;
            }

            return await _userRepository.Get(page, pageSize, filter, sort); ;
        }

        public async Task<User> Get(Guid id)
        {
            return await _userRepository.Get(id);
        }

        public async Task<User> Update(Guid id, User user)
        {
            var publisherDB = await _userRepository.Get(id);
            if (publisherDB != null)
            {
                publisherDB.Name = user.Name;
                publisherDB.Email = user.Email;
                publisherDB.Password = user.Password;
                publisherDB.ConfirmPassword = user.ConfirmPassword;
                return await _userRepository.Update(id, publisherDB);
            }
            return await _userRepository.Add(new User());
        }

        public async Task<UserAuth> Authenticate(string email, string password)
        {
            var user = await _userRepository.Get(email);

            if (user is null)
            {
                return null;
            }

            if (password != user.Password)
            {
                return null;
            }

            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, email)
            };

            return new UserAuth
            {
                Email = email
            };
         
        }
    }
}
