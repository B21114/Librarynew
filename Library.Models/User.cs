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
        [Required(ErrorMessage = "Не указан Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Пароль пользователя повторно.
        /// </summary>
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

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