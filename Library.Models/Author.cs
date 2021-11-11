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
    /// Класс автор книги.
    /// </summary>
    public class Author
    {
        public static Expression<Func<Author, object>> GetSortExpressions(string sortBy)
        {
            return sortBy?.ToLower() switch
            {
                "firstname" => p => p.Firstname,
                "lastname" => p => p.Lastname,
                "patronomic" => p => p.Patronymic,
                "activity" => p => p.Activity,
                _ => p => p.Id
            };
        }

        /// <summary>
        /// Идентификатор автора.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя автора.
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Отчество автора.
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// Фамилия автора.
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// Деятельность автора.
        /// </summary>
        public string Activity { get; set; }
    }
}
