using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    /// <summary>
    /// Класс книги.
    /// </summary>
    public class Book
    {

        /// <summary>
        /// Идентификатор книги.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование книги.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Количество страниц книги.
        /// </summary>
        public int NumberOfPages { get; set; }

        /// <summary>
        /// Коллекция авторов книги.
        /// </summary>
        public List<Author> Authors { get; set; }

        /// <summary>
        /// Издатель книги.
        /// </summary>
        public Publisher Publisher { get; set; }

        /// <summary>
        /// Метод для сортировки по полям.
        /// </summary>
        /// <param name="sortBy"></param>
        /// <returns></returns>
        public static Expression<Func<Book, object>> GetSortExpressions(string sortBy)
        {
            return sortBy?.ToLower() switch
            {
                "name" => p => p.Name,
                "numberofpages" => p => p.NumberOfPages,
                "authors" => p => p.Authors,
                "publisher" => p => p.Publisher,
                _ => p => p.Id
            };
        }
    }
}
