using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    /// <summary>
    /// Модель издателя.
    /// </summary>
    public class Publisher
    {
        /// <summary>
        /// Идентификатор издателя.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование издателя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Город издателя.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Метод для сортировки по полям.
        /// </summary>
        /// <param name="sortBy"></param>
        /// <returns></returns>
        public static Expression<Func<Publisher, object>> GetSortExpressions(string sortBy)
        {
            return sortBy?.ToLower() switch
            {
                "name" => p => p.Name,
                "city" => p => p.City,
                _ => p => p.Id
            };
        }
    }
}
