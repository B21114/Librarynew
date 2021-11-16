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
using System.IdentityModel.Tokens.Jwt;
using Library.BL.Config;
using System.Security.Cryptography;
using System.ComponentModel;

namespace Library.BL.Services.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly IUserRepository _userRepository;
        private readonly string _appSettings;

        public UserProvider(IUserRepository userRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _appSettings =appSettings.Value.Secret;
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
                publisherDB.PasswordStr = user.PasswordStr;
                publisherDB.ConfirmPasswordStr = user.ConfirmPasswordStr;
                return await _userRepository.Update(id, publisherDB);
            }
            return await _userRepository.Add(new User());
        }

        public async Task<User> Authenticate(string email, string password)
        {
            var user = await _userRepository.Get(email);
            var identity = GetIdentity(email, password);
            if (identity == null)
            {
                return null;
            }

            if (user is null)
            {
                return null;
            }
            if (password != user.PasswordStr)
            {
                return null;
            }
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Result.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new User
            {
                Token = encodedJwt,
                Name = identity.Result.Name
            };
        }

        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var user = await _userRepository.Get(username);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            // если пользователя не найдено
            return null;
        }
    }
}
