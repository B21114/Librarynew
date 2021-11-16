using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email пользователя.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public byte[] Password { get; set; }

        /// <summary>
        /// Пароль пользователя и соль.
        /// </summary>
        public byte[] PasswordSalt { get; set; }

        public string PasswordStr { get; set; }

        public string ConfirmPasswordStr { get; set; }


        /// <summary>
        /// Токен для авторизации.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Метод для сортировки по полям.
        /// </summary>
        /// <param name="sortBy"></param>
        /// <returns></returns>
        public static Expression<Func<User, object>> GetSortExpressions(string sortBy)
        {
            return sortBy?.ToLower() switch
            {
                "name" => p => p.Name,
                "email" => p => p.Email,
                _ => p => p.Id
            };
        }
    }
}