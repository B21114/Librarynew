using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BL.Services.Interfaces
{
    public interface IAuthentication
    {
        /// <summary>
        /// Метод для аутентификации, создает токен пользователя
        /// </summary>
        /// <param name="email">Email пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns></returns>
        Task<UserAuth> Authenticate(string email, string password);
    }
}
